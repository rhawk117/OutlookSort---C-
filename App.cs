using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using static System.Console;

namespace OutlookSort
{
    public class App
    {
        private string configPath;
        private void appSetUp()
        {
            try
            {
                string configDirectoryName = "config";
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                configPath = Path.Combine(baseDirectory, configDirectoryName);
                if (!Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                    WriteLine($"[i] Configuration directory created at: {configPath}");
                    WriteLine("[i] Create a preset to use program");
                }
                else
                {
                    WriteLine($"[i] Configuration directory found: {configPath}");
                }
            }
            catch (Exception ex)
            {
                WriteLine("[!] An error occurred while trying to set up the program... \n" + ex.Message);
            }
        }
        public App()
        {
            appSetUp();
        }
        private void mainMenuHndler()
        {
            var menu = new Menu
            (
                options: new string[] { "Create Preset", "Open Preset", "Exit" }, 
                prompt: "Main Menu"
            );
            int selection = menu.Run();
        }


    }
}
