namespace ztm_gdansk_stops_app.Server.Models;

public class User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? FavoriteStops { get; set; }
}