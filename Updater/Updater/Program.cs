using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceExe = @"C:\Users\Диана\source\repos\TOPO\CP_TOPO\Updater\Updater\bin\Debug\Updater.exe";

            string destinationExe = @"C:\Users\Диана\source\repos\TOPO\CP_TOPO\bin\Debug\CP_TOPO.exe";

            string sourceVersion = @"C:\Users\Диана\source\repos\TOPO\CP_TOPO\Updater\VersionInfo.cs";

            string destinationVersion = @"C:\Users\Диана\source\repos\TOPO\CP_TOPO\VersionInfo.cs";

            try
            {
                Thread.Sleep(2000);

                if (File.Exists(destinationExe))
                {
                    File.Delete(destinationExe);
                }

                File.Copy(sourceExe, destinationExe);

                File.Copy(sourceVersion, destinationVersion, true);

                Console.WriteLine("Обновление установлено");

                Process.Start(destinationExe);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
    }
    }
}
