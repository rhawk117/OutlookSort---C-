using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Outlook;
using static System.Console;


namespace OutlookSort
{
    public class App
    {
        private string configPath, txtPath;
        private NameSpace userNameSpace;
        private bool hasPresets
        {
            get => ( UserPresets.Count > 0 );
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
                
                WriteLine($@"
                ***********************************************************************************************
                                 [i] Configuration directory created at => {configPath} [i]

                                                   [!] NOTE [!]                

                        [i] To use the program's sorting functionality, you must create a preset first [i]

                                             ** Press Enter to Continue **

                *********************************************************************************************** 
                ");
            }
            else
            {
                UserPresets = Directory.GetFiles(configPath)
                        .Where(files => files.EndsWith(".json"))
                        .ToList();
            }
            if (!(Directory.Exists(txtPath)))
            {
                Directory.CreateDirectory(txtPath);
            }

           

        }
        
        public override string ToString() => 
            $"[ User Has Presets? => {hasPresets} ]" +
            $"\n[ Configuration Path => {configPath} ]" +
            $"\n[ Text Files Path => {txtPath} ]"; 


            

    }
}
