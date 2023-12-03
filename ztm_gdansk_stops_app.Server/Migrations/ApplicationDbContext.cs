using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ztm_gdansk_stops_app.Server.Models;

namespace ztm_gdansk_stops_app.Server.Migrations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbFileName = "ZTMDatabase.db";
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "ZTMDatabase.db" };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);

        optionsBuilder.UseSqlite(connection);
    }
}