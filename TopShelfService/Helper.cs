using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TopShelfService.Properties;

namespace TopShelfService
{
    public class Helper
    {
        private readonly Timer _timer;
        public Helper()
        {
            //timer 10s, mỗi 10s sẽ tự động reset
            var interval = Settings.Default.Interval;

            _timer = new Timer(interval == 0 ? 60 * 60 * 1000 : interval) { AutoReset = true };
            _timer.Elapsed += (sender, e) =>
            {
                //app name và string path
                //var appname = Settings.Default.AppName;
                //var path = Settings.Default.StringPath;
                //var appname2 = Settings.Default.AppName2;
                //var path2 = Settings.Default.StringPath2;

                //foreach (var process in Process.GetProcessesByName(appname))
                //{
                //    process.Kill();
                //    WriteLogError("Stop " + appname);
                //}
                //foreach (var process in Process.GetProcessesByName(appname2))
                //{
                //    process.Kill();
                //    WriteLogError("Stop " + appname2);
                //}
                //Process.Start(path);
                //Process.Start(path2);
                //WriteLogError("Start " + path + "," + path2);

            };
        }
        public void Start()
        {
            WriteLogError("Service Start");
            _timer.Start();

        }
        public void Stop()
        {
            WriteLogError("Service Stop");
            _timer.Stop();

        }
        // Ghi lại Log File ở các bước thực thi Service mỗi 60s
        public static void WriteLogError(string message)
        {
            StreamWriter sw = null;
            try
            {
                deleteLog(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt");
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString("g") + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // ignored
            }
        }
        //xóa log mỗi khi quá nhiều
        private static void deleteLog(string file_path)
        {
            try
            {
                if (new FileInfo(file_path).Length > Int32.MaxValue)
                {
                    File.WriteAllText(file_path, string.Empty);
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
