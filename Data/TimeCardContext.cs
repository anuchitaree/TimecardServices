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
         
            //Param.DbConnnectionString = "User ID =postgres;Server=localhost;Port=5432;Database=Timecard;Username=postgres;Password=admin;Integrated Security=true;Pooling=true;";
            
            optionsBuilder.UseNpgsql(Param.DbConnnectionString);


        }


    }
}
