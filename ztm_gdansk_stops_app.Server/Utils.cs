using ztm_gdansk_stops_app.Server.Migrations;
using ztm_gdansk_stops_app.Server.Models;

namespace ztm_gdansk_stops_app.Server;

public class Utils
{
    public static List<int> getStops(User user)
    {
        List<int> stops = new();

        if (user.FavoriteStops == null || user.FavoriteStops.Length == 0) return stops;

        if (user.FavoriteStops.Contains(' '))
            stops = user.FavoriteStops.Split(' ').Select(int.Parse).ToList();
        else
            stops.Add(int.Parse(user.FavoriteStops));

        return stops;
    }

    public static async void addStop(User user, int busStopId, ApplicationDbContext db)
    {
        user.FavoriteStops ??= "";

        var stops = getStops(user);

        if (!stops.Contains(busStopId))
        {
            if (stops.Count > 0) user.FavoriteStops += " ";
            user.FavoriteStops += busStopId;
            await db.SaveChangesAsync();
        }
    }


    public static async void deleteStop(User user, int stopId, ApplicationDbContext db)
    {
        user.FavoriteStops ??= "";

        List<int> stops = new();

        stops = getStops(user);

        if (stops.Contains(stopId))
        {
            stops = stops.Where(u => u != stopId).ToList();

            var stopsString = "";
            foreach (var busStop in stops) stopsString += busStop + " ";

            if (stopsString.Length > 0) stopsString = stopsString.Remove(stopsString.Length - 1);

            user.FavoriteStops = stopsString;
            await db.SaveChangesAsync();
        }
    }
}