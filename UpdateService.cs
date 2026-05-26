using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
    class UpdateService
    {
        private FileService _fileService =new FileService();
        private string clientVersion = @"C:\Users\Диана\source\repos\TOPO\CP_TOPO\VersionInfo.cs";
        private string serverVersion = @"C:\Users\Диана\source\repos\TOPO\CP_TOPO\Updater\VersionInfo.cs";

        public bool CheckForUpdates()
        {
            bool isEqual =_fileService.IsEqual(clientVersion, serverVersion);
            if (isEqual)
            {
                Console.WriteLine("Обновление не требуется");
                return false;
            }
            Console.WriteLine("Обнаружена новая версия");
            return true;
        }
        public void ApplyUpdate()
        {
            string updater = @"C:\Users\Диана\source\repos\TOPO\CP_TOPO\Updater\Updater\bin\Debug\Updater.exe";
            if (!File.Exists(updater))
            {
                Console.WriteLine("Установщик обновлений не найден");
                return;
            }
            Console.WriteLine("Запуск обновления...");
            Process.Start(updater);
            Environment.Exit(0);
        }
        //public void UpdateAll()
        //{
        //    if (!File.Exists(destinationFile) || !File.Exists(targetFile))
        //    {
        //        Console.WriteLine("Один из файлов не найден");
        //        return;
        //    }

        //    if (_fileService.IsEqual(destinationFile, targetFile))
        //    {
        //        Console.WriteLine("Уже актуальная версия");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Обновление...");
        //        _fileService.UpdateFile(targetFile, destinationFile);
        //    }
        //}
    }
}
