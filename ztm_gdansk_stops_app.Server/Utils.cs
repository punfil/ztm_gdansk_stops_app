using ztm_gdansk_stops_app.Server.Migrations;
using ztm_gdansk_stops_app.Server.Models;

namespace ztm_gdansk_stops_app.Server;

public class Utils
{
    public static List<int> getBusStops(User user)
    {
        List<int> busStops = new();

        if (user.BusStops == null || user.BusStops.Length == 0) return busStops;

        if (user.BusStops.Contains(' '))
            busStops = user.BusStops.Split(' ').Select(int.Parse).ToList();
        else
            busStops.Add(int.Parse(user.BusStops));

        return busStops;
    }

    public static async void addBusStop(User user, int busStopId, ApplicationDbContext db)
    {
        user.BusStops ??= "";

        var busStops = getBusStops(user);

        if (!busStops.Contains(busStopId))
        {
            if (busStops.Count > 0) user.BusStops += " ";
            user.BusStops += busStopId;
            await db.SaveChangesAsync();
        }
    }


    public static async void deleteBusStop(User user, int busStopId, ApplicationDbContext db)
    {
        user.BusStops ??= "";

        List<int> busStops = new();

        busStops = getBusStops(user);

        if (busStops.Contains(busStopId))
        {
            busStops = busStops.Where(u => u != busStopId).ToList();

            var busStopsString = "";
            foreach (var busStop in busStops) busStopsString += busStop + " ";

            if (busStopsString.Length > 0) busStopsString = busStopsString.Remove(busStopsString.Length - 1);

            user.BusStops = busStopsString;
            await db.SaveChangesAsync();
        }
    }
}