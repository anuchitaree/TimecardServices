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
            optionsBuilder.UseSqlServer(Param.DbConnnectionString);
        }


    }
}
