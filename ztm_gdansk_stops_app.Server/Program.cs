using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ztm_gdansk_stops_app.Server;
using ztm_gdansk_stops_app.Server.Migrations;
using ztm_gdansk_stops_app.Server.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

var builder = WebApplication.CreateBuilder(args);
var client = new HttpClient();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite());
builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var memoryCache = app.Services.GetService<IMemoryCache>();


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/adduser", async (User user, ApplicationDbContext db) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/adduser/{user.Login}", user);
});

app.MapDelete("/deleteuser/{id}", async (int id, ApplicationDbContext db) =>
{
    if (await db.Users.FindAsync(id) is User user)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.Ok(user);
    }

    return Results.NotFound();
});


app.MapPost("/login", async (User user, ApplicationDbContext db) =>
{
    try
    {
        var userFromDb = await db.Users.FirstAsync(u => u.Login == user.Login && u.Password == user.Password);

        if (userFromDb != null)
        {
            var data = new { id = userFromDb.Id };
            return Results.Ok(data);
        }

        return Results.NotFound();
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});


app.MapGet("/listusers", async (ApplicationDbContext db) =>
    await db.Users.ToListAsync());

app.MapGet("/listuserbusstops/{userId}", async (int userId, ApplicationDbContext db) =>
{
    var user = await db.Users.FirstAsync(u => u.Id == userId);
    var stopsId = Utils.getStops(user);

    var arrayWraps = new List<UserData>();

    foreach (var stopId in stopsId ?? new List<int>())
    {
        var jsonResponse = await fetchDataFromUri($"http://ckan2.multimediagdansk.pl/delays?stopId={stopId}");
        var infoArray = JsonConvert.DeserializeObject<UserDataCKAN>(jsonResponse ?? "")?.delay;

        var arrayWrap = new UserData
        {
            Delays = infoArray!,
            StopId = stopId
        };

        if (infoArray != null) arrayWraps.Add(arrayWrap);
    }

    return JsonSerializer.Serialize(arrayWraps);
});

app.MapGet("/stopinfo/{stopId}", async (int stopId) =>
    await memoryCache!.GetOrCreateAsync($"stop_{stopId}",
        async entry =>
        {
            return Results.Content(await fetchDataFromUri($"http://ckan2.multimediagdansk.pl/delays?stopId={stopId}"));
        })
);

app.MapGet("/stops", async () =>
    await memoryCache!.GetOrCreateAsync("stopinfo",
        async entry =>
        {
            return await fetchDataFromUri(
                "https://ckan.multimediagdansk.pl/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/4c4025f0-01bf-41f7-a39f-d156d201b82b/download/stops.json");
        })
);

// from query
app.MapPost("/addstop", async (int userId, int stopId, ApplicationDbContext db) =>
{
    try
    {
        var user = await db.Users.FirstAsync(u => u.Id == userId);

        Utils.addStop(user, stopId, db);

        return Results.Ok();
    }
    catch (Exception)
    {
        return Results.BadRequest();
    }
});

async Task<string?> fetchDataFromUri(string uri)
{
    var requestContent = (await client.GetAsync(uri)).Content;
    var jsonContent = await requestContent.ReadAsStringAsync();

    return jsonContent;
}

app.MapDelete("/deletestop", async (int userId, int stopId, ApplicationDbContext db) =>
{
    try
    {
        var user = await db.Users.FirstAsync(u => u.Id == userId);

        Utils.deleteStop(user, stopId, db);

        return Results.Ok();
    }
    catch (Exception)
    {
        // or internalServerError
        return Results.BadRequest();
    }
});

app.MapGet("/", () => "Hello World!");

app.MapFallbackToFile("/index.html");

app.Run();