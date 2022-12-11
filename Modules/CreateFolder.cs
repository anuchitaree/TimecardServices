using Newtonsoft.Json;
using TimecardServices.DTO;

namespace TimecardServices.Modules
{
    public class CreateFolder
    {
        public static void IsFolder()
        {
            try
            {
                Param.FromFolder = $"{Param.BaseFolder}\\sources";

                Param.ProcessFolder= $"{Param.BaseFolder}\\.proc";

                Param.InstallFolder = $"{Param.BaseFolder}\\dotnet";

                Param.HistoryFolder = $"{Param.BackupFolder}\\history";

                if (!Directory.Exists(Param.BaseFolder))
                    Directory.CreateDirectory(Param.BaseFolder);

                if (!Directory.Exists(Param.FromFolder))
                    Directory.CreateDirectory(Param.FromFolder);


                if (!Directory.Exists(Param.InstallFolder))
                    Directory.CreateDirectory(Param.InstallFolder);

                if (!Directory.Exists(Param.ProcessFolder))
                    Directory.CreateDirectory(Param.ProcessFolder);

                //string settingFile = Environment.CurrentDirectory + "\\www\\conf\\settings.json";

                //if (File.Exists(settingFile))
                //{
                //    using (StreamReader r = new StreamReader(settingFile))
                //    {
                //        string json = r.ReadToEnd();
                //        List<Settings> items = JsonConvert.DeserializeObject<List<Settings>>(json)!;
                //        Param.HttpPostUrl = items[0].HttpPostUrl;
                //        Param.HistoryOnOff = items[0].HistoryOnOff;
                //        Param.BackupFolder = items[0].BackupFolderName;
                //        Param.ScanLoopTime = Convert.ToInt32(items[0].ScanLoopTime);
                //    }
                //}
                //else
                //{
                //    var settingdata = new List<Settings>();
                //    settingdata.Add(new Settings()
                //    {
                //        HttpPostUrl = Param.HttpPostUrl,
                //        HistoryOnOff = true,
                //        BackupFolderName = @"C:\TaffTimecard",
                //        ScanLoopTime = 60,
                //    });
                //    string json = JsonConvert.SerializeObject(settingdata.ToArray(), Formatting.Indented);
                //    File.WriteAllText(settingFile, json);
                //}


                if (!Directory.Exists(Param.BackupFolder))
                    Directory.CreateDirectory(Param.BackupFolder);

                //string backup = String.Format($"{Param.BackupFolder}\\history");
                
                if (!Directory.Exists(Param.HistoryFolder))
                    Directory.CreateDirectory(Param.HistoryFolder);
                
                string year = DateTime.Now.ToString("yyyy");
                
                Param.HistoryFolder = String.Format($"{Param.HistoryFolder}\\{year}");
                
                if (!Directory.Exists(Param.HistoryFolder))
                    Directory.CreateDirectory(Param.HistoryFolder);



            }
            catch
            {

            }

        }




    }
}
