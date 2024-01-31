using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using static System.Console;
using Microsoft.Office.Interop.Outlook;


namespace OutlookSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Application outlookApp = new Application();
            //NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
            //MAPIFolder inbox = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
            //var Test = new Filter(
            //    new List<string>() { "Augusta", "3320", "Jag" },  // Subject lines
            //    new List<string>() {  // Email addresses
            //        "aueventsweekly@e.augusta.edu",
            //        "noreply@augusta.edu",
            //        "jagwire@e.augusta.edu"
            //    },
            //    new List<string>() { "Test" }  // Folder names
            //);
            //WriteLine("running apply method");
            //Test.Apply(inbox);
            //WriteLine("\n\napply method done, emails found below \n"); ReadLine();
            //Test.DisplayEmailsMoved();

            // Main routine prototype
            var app = new App();
            app.MainMenu();
            

        }
    }
}
