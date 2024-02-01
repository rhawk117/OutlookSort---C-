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
            WriteLine("");
            for (int i = 0; i < Options.Length; i++)
            {

                string currentOption = Options[i], prefix = "";
                if (i == SelectedIndex)
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
                WriteLine($"{prefix} [ " + currentOption + " ]");
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
                switch (keyPressed)
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
    public static class MainMenu
    {
        private const string TitleText = @"
        ===========================================================================================================
                                                                                                                       
         @@@@@@   @@@  @@@  @@@@@@@  @@@        @@@@@@    @@@@@@   @@@  @@@      @@@@@@    @@@@@@   @@@@@@@   @@@@@@@  
        @@@@@@@@  @@@  @@@  @@@@@@@  @@@       @@@@@@@@  @@@@@@@@  @@@  @@@     @@@@@@@   @@@@@@@@  @@@@@@@@  @@@@@@@  
        @@!  @@@  @@!  @@@    @@!    @@!       @@!  @@@  @@!  @@@  @@!  !@@     !@@       @@!  @@@  @@!  @@@    @@!    
        !@!  @!@  !@!  @!@    !@!    !@!       !@!  @!@  !@!  @!@  !@!  @!!     !@!       !@!  @!@  !@!  @!@    !@!    
        @!@  !@!  @!@  !@!    @!!    @!!       @!@  !@!  @!@  !@!  @!@@!@!      !!@@!!    @!@  !@!  @!@!!@!     @!!    
        !@!  !!!  !@!  !!!    !!!    !!!       !@!  !!!  !@!  !!!  !!@!!!        !!@!!!   !@!  !!!  !!@!@!      !!!    
        !!:  !!!  !!:  !!!    !!:    !!:       !!:  !!!  !!:  !!!  !!: :!!           !:!  !!:  !!!  !!: :!!     !!:    
        :!:  !:!  :!:  !:!    :!:     :!:      :!:  !:!  :!:  !:!  :!:  !:!         !:!   :!:  !:!  :!:  !:!    :!:    
        ::::: ::  ::::: ::     ::     :: ::::  ::::: ::  ::::: ::   ::  :::     :::: ::   ::::: ::  ::   :::     ::    
         : :  :    : :  :      :     : :: : :   : :  :    : :  :    :   :::     :: : :     : :  :    :   : :     : 

                                            *** created by rhawk117 *** 
                                        
        ===========================================================================================================
            
        ";

        public static void Run(bool havePresets)
        {
            Display(havePresets);
        }

        private static void Display(bool hasPresets)
        {
            string[] menuOptions = hasPresets
                ? new string[] { "Create Preset", "Help", "Exit" }
                : new string[] { "Create Preset", "Open Preset", "Help", "Exit" };

            var menu = new Menu
            (
                options: menuOptions,
                prompt: (TitleText) + 
                        "Welcome To Outlook Pal, Select a Menu Option To Continue"
            );

            int selection = menu.Run();
            mainMenuHndler(menuOptions[selection]);
        }
        private static void mainMenuHndler(string option)
        {
            switch (option)
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
                    WriteLine("[i] Exiting the program... [i]");
                    Environment.Exit(0);
                    break;

                default:
                    WriteLine("[!] Invalid selection, please try again");
                    break;
            }
        }

        
    }

}
    
    

