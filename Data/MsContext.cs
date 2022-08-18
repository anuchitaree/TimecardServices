using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardServices.Models;

namespace TimecardServices.Data
{
    public class MsContext : DbContext
    {

        public DbSet<MpTimecardRecord> MpTimecardRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=49.229.106.33\\SQLEXPRESS,10000;user id=Admin;password=Admin; Database =mp_timecard");
         
        }


    }
}
