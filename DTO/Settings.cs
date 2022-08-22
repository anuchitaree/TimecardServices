using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardServices.DTO
{
    internal class Settings
    {
        public string UploadUrl { get; set; } = null!;
        public string BackupFolderName { get; set; } = null!;
        public bool HistoryOnOff { get; set; } =true;
        public Int32 ScanTime { get; set; } 
    }
}
