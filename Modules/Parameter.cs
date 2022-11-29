using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardServices.Modules
{
    internal static class Parameter
    {
        public const  string BaseFolder = @"C:\TaffTimecard";
        public static string FromFolder = String.Format($"{BaseFolder}\\sources");


        // Fix
        public const string InstallFolder = @"C:\TaffTimecard";
        public static string ProcessFolder = String.Format($"{InstallFolder}\\.proc");


        public static bool HistoryOnOff = true; //depend on user
        public static string BackupFolder = @"D:\TaffBackup";  //depend on user
        public static string HistoryFolder = "history";

        public static string UploadUrl = "https://localhost:6557/api/MPCalculation/Timecard/recordtime"; //"http://   /transfer_function_raw_data";   //depend on user

        public static Int32 Scantime = 300;

        public static Int32 UploadProdRecord = 60;


    }
}

