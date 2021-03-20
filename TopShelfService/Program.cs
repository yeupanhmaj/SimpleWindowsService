using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TopShelfService
{
    class Program
    {
        static void Main(string[] args)
        {
            //khởi tạo service
            //chạy liên tục tới khi nào app tắt
            var exitCode = HostFactory.Run(sc =>
            {
                sc.Service<Helper>(s =>
                {
                    s.ConstructUsing(helper => new Helper());
                    s.WhenStarted(helper => helper.Start());
                    s.WhenStopped(helper => helper.Stop()); 
                });
                sc.RunAsLocalSystem();
                sc.SetServiceName("ExecuteHelper");
                sc.SetDisplayName("ExecuteHelper");
                sc.SetDescription("Run exe file!");
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
