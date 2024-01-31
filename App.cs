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
        private string configPath, txtPath;
        private bool hasPresets
        {
            get => UserPresets.Count > 0;
        }

        private List<string> _userPresets;
        public List<string> UserPresets
        {
            get => _userPresets;
            set
            {
                if(Directory.Exists(configPath))
                {
                    _userPresets = Directory.GetFiles(configPath)
                        .Where(files => files.EndsWith(".json"))
                        .ToList();
                }
                else
                {
                    WriteLine("[!] Configuration directory not found, cannot set presets");
                }
            }
        }
        public App()
        {
            const string configDirectoryName = "config", textDir = "txt_files";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;


            configPath = Path.Combine(baseDirectory, configDirectoryName);
            txtPath = Path.Combine(configPath, textDir);
            UserPresets = new List<string>();
            this.DirectoryChecks();
        }
        private void DirectoryChecks()
        {
            if (!(Directory.Exists(configPath)))
            {
                Directory.CreateDirectory(configPath);
                WriteLine($"[i] Configuration directory created at: {configPath}");
                WriteLine("[i] To use the program's sorting functionality create a preset in the Main Menu\nPress Enter to continue");
                
            }
            else
            {
                UserPresets = Directory.GetFiles(configPath)
                        .Where(files => files.EndsWith(".json"))
                        .ToList();
                WriteLine($"[i] Configuration was directory as found at => {configPath} with {UserPresets.Count} created.");
            }

            if (!(Directory.Exists(txtPath)))
            {
                Directory.CreateDirectory(txtPath);
            }

            WriteLine("[i] Press Enter to continue... [i]");
            ReadKey();
        }
        public void MainMenu()
        {
            string[] menuOptions;

            if (hasPresets)
                menuOptions = new string[] { "Create Preset", "Help", "Exit" };
            else
                menuOptions = new string[] { "Create Preset", "Open Preset", "Help", "Exit" };

            var menu = new Menu
            (
                options: menuOptions, 
                prompt: "Welcome Please Select a Menu Option"
            );

            int selection = menu.Run();

            // handle user selection
            switch(menuOptions[selection])
            {
                case "Create Preset":
                    WriteLine("[i] Creating a new preset...");
                    break;

                case "Open Preset":
                    WriteLine("[i] Opening a preset...");
                    break;

                case "Help":
                    WriteLine("[i] Displaying help...");
                    break;

                case "Exit":
                    WriteLine("[i] Exiting the program...");
                    Environment.Exit(0);
                    break;

                default:
                    WriteLine("[!] Invalid selection, please try again");
                    break;
            }
        }

        public override string ToString() => 
            $"[ User Has Presets? => {hasPresets} ]" +
            $"\n[ Configuration Path => {configPath} ]" +
            $"\n[ Text Files Path => {txtPath} ]"; 


            

    }
}
