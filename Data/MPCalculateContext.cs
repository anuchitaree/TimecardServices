using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardServices.Models;

namespace TimecardServices.Data
{
    public class MPCalculateContext: DbContext
    {

        public DbSet<MpCalculateTimecardRecord> MpCalculateTimecardRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS,1433;user id=Admin;password=Admin; Database =timecard");

            optionsBuilder.UseNpgsql("Server = localhost;User Id = postgres;Password = admin;Port=5432;Database = MpCalcuateTimecard;Pooling = false;Timeout = 300;CommandTimeout = 300");


        }

    }
}
