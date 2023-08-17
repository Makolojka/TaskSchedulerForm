using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    internal class FolderUtils
    {
        // TODO: zmienić sposóp sprawdzania posiadanych przez użytkownika uprawnień
        public static bool CanAccessFolder(string folderPath)
        {
            try
            {
                // Tworzy pusty plik i go usuwa
                string testFilePath = Path.Combine(folderPath, "test.txt");
                File.WriteAllText(testFilePath, "test");
                File.Delete(testFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
