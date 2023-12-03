namespace ztm_gdansk_stops_app.Server.Models;

public class UserData
{
    public List<Delay> Delays { get; set; }
    public int StopId { get; set; }
    public string StopName { get; set; }
}
