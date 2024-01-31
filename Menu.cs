using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace OutlookSort
{
    public class Menu
    {
        public string[] Options { get; set; }
        public string Prompt { get; set; }
        private int selectedIndex;
        private int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (value < 0)
                    selectedIndex = Options.Length - 1;
                else if (value >= Options.Length)
                    selectedIndex = 0;
                else
                    selectedIndex = value;
            }
        }
        public Menu(string[] options, string prompt)
        {
            this.Options = options;
            this.Prompt = prompt;
            this.SelectedIndex = 0;
        }
        private void Show()
        {
            Clear();
            WriteLine("[" + Prompt + "]");
            for (int i = 0; i < Options.Length; i++)
            {
                
                string currentOption = Options[i], prefix = "";  
                if(i == SelectedIndex)
                {
                    prefix = "> ";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{prefix} [ "+ currentOption + " ]");
            }
            ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                Show();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                switch(keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        break;
                }
            } 
            while (keyPressed != ConsoleKey.Enter);
            return selectedIndex;
        }   
    }
    public class MainMenu
    {

        private string titleText = @"   ___  _   _ _____ _     ___   ___  _  __  ____   _    _     
                                      / _ \| | | |_   _| |   / _ \ / _ \| |/ / |  _ \ / \  | |    
                                     | | | | | | | | | | |  | | | | | | | ' /  | |_) / _ \ | |    
                                     | |_| | |_| | | | | |__| |_| | |_| | . \  |  __/ ___ \| |___ 
                                      \___/ \___/  |_| |_____\___/ \___/|_|\_\ |_| /_/   \_\_____|";



        
    }
    
}
