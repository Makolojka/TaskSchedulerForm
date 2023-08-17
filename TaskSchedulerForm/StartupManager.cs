using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    internal class StartupManager
    {
        public static void CreateShortcutInStartup()
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, "HarmonogramMK.lnk");

            if (!File.Exists(shortcutPath))
            {
                try
                {
                    string appPath = Assembly.GetExecutingAssembly().Location;

                    using (StreamWriter writer = new StreamWriter(shortcutPath))
                    {
                        writer.WriteLine("[InternetShortcut]");
                        writer.WriteLine("URL=file:///" + appPath);
                        writer.WriteLine("IconIndex=0");
                        writer.WriteLine("IconFile=" + appPath);
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas tworzenia skrótu aplikacji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void RemoveShortcutFromStartup()
        {
            try
            {
                string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                string shortcutPath = Path.Combine(startupFolderPath, "HarmonogramMK.lnk");

                if (File.Exists(shortcutPath))
                {
                    File.Delete(shortcutPath);
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas usuwania skrótu aplikacji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
