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
            //initial service
            //run over and over until program close
            var exitCode = HostFactory.Run(sc =>
            {
                sc.Service<Helper>(s =>
                {
                    s.ConstructUsing(helper => new Helper());
                    s.WhenStarted(helper => helper.Start());
                    s.WhenStopped(helper => helper.Stop());
                });
                sc.RunAsLocalSystem();
                //your service name
                sc.SetServiceName("ExecuteHelper");
                //your display name
                sc.SetDisplayName("ExecuteHelper");
                //your description 
                sc.SetDescription("Run exe file!");
            });
            //get exitcode
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
