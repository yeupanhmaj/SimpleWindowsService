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
            //interval for timmer, you can setup manually or using config file
            var interval = Settings.Default.Interval;
            //initial timer,auto reset when elapsed
            _timer = new Timer(interval == 0 ? 60 * 60 * 1000 : interval) { AutoReset = true };
            //on elapsed event, i use lambda, you can change it to normal delegate if you want
            _timer.Elapsed += (sender, e) =>
            {
                //do some thing on timer elapsed

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
        //write log
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
        //delete log when to big
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
