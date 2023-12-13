using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ztm_gdansk_stops_app.Server;
using ztm_gdansk_stops_app.Server.Migrations;
using ztm_gdansk_stops_app.Server.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
var client = new HttpClient();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite());
builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = "your-hardcoded-audience",
            ValidIssuer = "your-hardcoded-issuer",
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-hardcoded-key"))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddAuthorization();

var app = builder.Build();
var memoryCache = app.Services.GetService<IMemoryCache>();

app.UseAuthentication();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/adduser/{id}&{username}&{password}", async (int id, string username, string password, ApplicationDbContext db) =>
{
    var user = new User(id, username, password);
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


app.MapPost("/login/{username}&{password}", async (string username, string password, ApplicationDbContext db) =>
{
    try
    {
        var userFromDb = await db.Users.FirstAsync(u => u.Login == username && u.Password == password);

        if (userFromDb != null)
        {
            // Generate JWT token
            var usernameClaim = new Claim("id", userFromDb.Id.ToString());
            var securityKey = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-hardcoded-keyasdasdasdadasdasdad"));
            var credentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);
            var tokenstr = new JwtSecurityToken("your - hardcoded - issuesaaaaaaaaaaaaa",
                "your-hardcoded-audienceaaaaaaaaaaaaa",
                new List<Claim> { usernameClaim },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenstr);
            // Return the token in the response
            var data = new { id = userFromDb.Id, token = tokenString };
            return Results.Ok(data);
        }

        return Results.NotFound();
    }
    catch (Exception)
    {
        return Results.NotFound();
    }
});


app.MapGet("/listusers",  (ApplicationDbContext db) =>
{
    var list = db.Users.ToArray();
    return JsonSerializer.Serialize(list);
});


app.MapGet("/listuserstops/{userId}", async (HttpContext context, int userId, ApplicationDbContext db) =>
{
    var user = await db.Users.FirstAsync(u => u.Id == userId);
    var stopsId = Utils.getStops(user);

    if (!VerifyToken(context.Request.Headers["Authorization"].FirstOrDefault(), userId))
    {
        return null;
    }

    var arrayWraps = new List<UserData>();

    foreach (var stopId in stopsId ?? new List<int>())
    {
        var jsonResponse = await fetchDataFromUri($"http://ckan2.multimediagdansk.pl/delays?stopId={stopId}");
        var infoArray = JsonConvert.DeserializeObject<UserDataCKAN>(jsonResponse ?? "");

        var arrayWrap = new UserData
        {
            Delays = infoArray.delay,
            StopId = stopId,
            LastUpdate = infoArray.lastUpdate
        };

        if (infoArray != null) arrayWraps.Add(arrayWrap);
    }

    return arrayWraps;
});

app.MapGet("/stopinfo/{stopId}", async (int stopId) =>
    await memoryCache!.GetOrCreateAsync($"stop_{stopId}",
        async entry =>
        {
            return Results.Content(await fetchDataFromUri($"http://ckan2.multimediagdansk.pl/delays?stopId={stopId}"));
        })
);

app.MapGet("/stops", async () =>
{
    var list = await memoryCache!.GetOrCreateAsync("stopinfo",
        async entry =>
        {
            return await fetchDataFromUri(
                "https://ckan.multimediagdansk.pl/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/4c4025f0-01bf-41f7-a39f-d156d201b82b/download/stops.json");
        });
    return list;
});

app.MapPost("/addstop/{userId}&{stopId}", async (int userId, int stopId, ApplicationDbContext db) =>
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

app.MapDelete("/deletestop/{userId}&{stopId}", async (int userId, int stopId, ApplicationDbContext db) =>
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

bool VerifyToken(string token, int userId)
{
    try
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new HMACSHA512(Encoding.UTF8.GetBytes("your-hardcoded-keyasdasdasdadasdasdad"));
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(key.Key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            RequireSignedTokens = false,
            RequireExpirationTime = false
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var claims = jwtToken.Claims;
        if (claims.First(x => x.Type == "id").Value != userId.ToString())
        {
            throw new Exception();
        }
    }
    catch (Exception)
    {
        return false;
    }

    return true;
    
}