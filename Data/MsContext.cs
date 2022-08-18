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
            optionsBuilder.UseSqlServer("Server=0.0.0.0\\SQLEXPRESS,0;user id=Admin;password=Admin; Database =mp_timecard");
         
        }


    }
}
