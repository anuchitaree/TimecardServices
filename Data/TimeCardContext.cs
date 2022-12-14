using Microsoft.EntityFrameworkCore;
using TimecardServices.Models;
using TimecardServices.Modules;

namespace TimecardServices.Data
{
    public class TimeCardContext : DbContext
    {
        public DbSet<TimecardRecord> TimecardRecords { get; set; } = null!;
        public DbSet<LogRecord> LogRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Param.DbConnnectionString = "Server = localhost\\SQLEXPRESS,1433; user id = Admin; password = Admin; Database = Timecard; Encrypt = True; TrustServerCertificate = True";
           
            optionsBuilder.UseSqlServer(Param.DbConnnectionString);

        }


    }
}
