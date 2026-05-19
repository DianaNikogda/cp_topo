using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
    class SystemService
    {
        public void GetSystemStats()
        {
            Console.WriteLine("Системная информация:");

            Console.WriteLine($"CPU (логические ядра): {Environment.ProcessorCount}");

            var ram = GC.GetTotalMemory(true);
            Console.WriteLine($"Память (используется): {GC.GetTotalMemory(false) / 1024 / 1024} MB");

            Console.WriteLine($"OS: {Environment.OSVersion}");
            Console.WriteLine($"Machine: {Environment.MachineName}");
        }
    }
}
