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
                if (!Directory.Exists(Parameter.BaseFolder))
                    Directory.CreateDirectory(Parameter.BaseFolder);
                if (!Directory.Exists(Parameter.FromFolder))
                    Directory.CreateDirectory(Parameter.FromFolder);


                if (!Directory.Exists(Parameter.InstallFolder))
                    Directory.CreateDirectory(Parameter.InstallFolder);
                if (!Directory.Exists(Parameter.ProcessFolder))
                    Directory.CreateDirectory(Parameter.ProcessFolder);


                string settingFile = String.Format($"{Parameter.InstallFolder}\\settings.json");
                if (File.Exists(settingFile))
                {
                    using (StreamReader r = new StreamReader(settingFile))
                    {
                        string json = r.ReadToEnd();
                        List<Settings> items = JsonConvert.DeserializeObject<List<Settings>>(json)!;

                        Parameter.HistoryOnOff = items[0].HistoryOnOff;
                        Parameter.BackupFolder = items[0].BackupFolderName;
                        Parameter.UploadUrl = items[0].UploadUrl;
                        Parameter.Scantime = Convert.ToInt32(items[0].ScanTime);
                    }
                }
                else
                {
                    var settingdata = new List<Settings>();
                    settingdata.Add(new Settings()
                    {
                        HistoryOnOff = true,
                        BackupFolderName = @"C:\TaffTimecard",
                        UploadUrl = "http://localhost:6500/api/example.com",
                        ScanTime=60,
                    });
                    string json = JsonConvert.SerializeObject(settingdata.ToArray(), Formatting.Indented);
                    File.WriteAllText(settingFile, json);
                }


                if (!Directory.Exists(Parameter.BackupFolder))
                    Directory.CreateDirectory(Parameter.BackupFolder);
                string backup = String.Format($"{Parameter.BackupFolder}\\history");
                if (!Directory.Exists(backup))
                    Directory.CreateDirectory(backup);
                string year = DateTime.Now.ToString("yyyy");
                Parameter.HistoryFolder = String.Format($"{backup}\\{year}");
                if (!Directory.Exists(Parameter.HistoryFolder))
                    Directory.CreateDirectory(Parameter.HistoryFolder);



            }
            catch
            {

            }

        }




    }
}
