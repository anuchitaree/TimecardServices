namespace TimecardServices.Modules
{
    internal static class Param
    {
        // c:\TaffTimecard
        public static string BaseFolder = "";

        // Keep .taf from TAFF Machine c:\TaffTimecard\sources
        public static string FromFolder = String.Empty; // String.Format($"{BaseFolder}\\sources");

        // Processing on this folder c:\TaffTimecard\.proc
        public static string ProcessFolder = String.Empty; //  String.Format($"{BaseFolder}\\.proc");

        // Processing on this folder  c:\TaffTimecard\dotnet
        public static string InstallFolder = String.Empty; //  String.Format($"{BaseFolder}\\dotnet");

        public static bool HistoryOnOff = true; //depend on user

        public static string HttpPostUrl = ""; //"http://   /transfer_function_raw_data";   //depend on user

        public static Int32 ScanLoopTime = 300;  // 300 second

        //"Server=localhost\\SQLEXPRESS,1433;user id=Admin;password=Admin; Database =Timecard;Encrypt=True;TrustServerCertificate=True";
        public static string DbConnnectionString = "";


        // if HistoryOnOff is true, files are kept here c:\TaffTimecard\
        public static string BackupFolder = "";  //depend on user

        // if HistoryOnOff is true, files are kept here c:\TaffTimecard\history
        public static string HistoryFolder = String.Empty; //  $"{BackupFolder}\\history";





    }
}

