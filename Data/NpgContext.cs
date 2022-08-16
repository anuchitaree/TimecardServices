using Microsoft.EntityFrameworkCore;
using TimecardServices.Models;

namespace TimecardServices.Data
{
    public class NpgContext : DbContext
    {

        public DbSet<TimecardRecord> TimecardRecords { get; set; } = null!;
        public DbSet<LogRecord> LogRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS,1433;user id=Admin;password=Admin; Database =timecard");

            optionsBuilder.UseNpgsql("Server = localhost;User Id = postgres;Password = admin;Port=5432;Database = timecard;Pooling = false;Timeout = 300;CommandTimeout = 300");


        }


    }
}
