using System.ComponentModel.DataAnnotations;

namespace ztm_gdansk_stops_app.Server.Models;
public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
