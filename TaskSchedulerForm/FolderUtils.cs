using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    internal class FolderUtils
    {
        public static bool CanAccessFolder(string folderPath)
        {
            try
            {
                // Attempt to create a dummy file in the folder
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
