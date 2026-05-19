using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
    class UpdateService
    {
        private FileService _fileService = new FileService();

        private string destinationFile = @"C:\Users\Диана\source\repos\TOPO\Destination\file1.txt";
        private string targetFile = @"C:\Users\Диана\source\repos\TOPO\Target\file1.txt";

        public bool CheckForUpdates()
        {
            if (!File.Exists(destinationFile) || !File.Exists(targetFile))
            {
                Console.WriteLine("Один из файлов не найден");
                return false;
            }

            bool isEqual = _fileService.IsEqual(destinationFile, targetFile);

            if (isEqual)
            {
                Console.WriteLine("Файлы совпадают. Обновление не требуется.");
                return false;
            }
            else
            {
                Console.WriteLine("Доступно обновление");
                return true;
            }
        }

        public void UpdateAll()
        {
            if (!File.Exists(destinationFile) || !File.Exists(targetFile))
            {
                Console.WriteLine("Один из файлов не найден");
                return;
            }

            if (_fileService.IsEqual(destinationFile, targetFile))
            {
                Console.WriteLine("Уже актуальная версия");
            }
            else
            {
                Console.WriteLine("Обновление...");
                _fileService.UpdateFile(targetFile, destinationFile);
            }
        }
    }
}
