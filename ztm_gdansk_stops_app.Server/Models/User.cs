namespace ztm_gdansk_stops_app.Server.Models;

public class User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? FavoriteStops { get; set; }

    public User(int id, string login, string password)
    {
        this.Id = id;
        this.Login = login;
        this.Password = password;
        this.FavoriteStops = "";
    }
}