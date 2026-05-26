using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
   public class FileService
    {
        private HashService hashService = new HashService();

        public bool IsEqual(string file1, string file2)
        {
            string hash1 = hashService.GetFileMD5(file1);
            string hash2 = hashService.GetFileMD5(file2);

            return hash1 == hash2 && hash1 != string.Empty;
        }

        public void UpdateFile(string target, string destination)
        {
            try
            {
                if (File.Exists(destination))
                    File.Delete(destination);

                File.Copy(target, destination, true);
                Console.WriteLine("Файл обновлён");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обновления: {ex.Message}");
            }
        }
    }
}
