using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using Newtonsoft.Json;
using static System.Console;

namespace OutlookSort
{
    public class Filter
    {
        public List<string> SubjectLines { get; set; }
        public List<string> EmailAddresses { get; set; }
        public List<string> FolderName { get; set; }
        public List<MailItem> FoundEmails { get; set; }
        public Filter(List<string> subjects, List<string> emails, List<string> folder)
        {
            this.SubjectLines = subjects;
            this.EmailAddresses = emails;
            this.FolderName = folder;
        }
        public Dictionary<string, List<string>> ToDictionary()
        {
            return new Dictionary<string, List<string>>
            {
                { "SubjectLines", this.SubjectLines },
                { "EmailAddresses", this.EmailAddresses },
                { "FolderName", this.FolderName }
            };
        }

        public void Apply(MAPIFolder usersOutlookInbox)
        {
            HashSet<string> uniqueEmailIds = new HashSet<string>();


            string filter;
            FoundEmails = EmailAddresses
                .SelectMany(emailAddress =>
                {
                    filter = $"[SenderEmailAddress] = '{emailAddress}'";
                    return usersOutlookInbox.Items.Restrict(filter).OfType<MailItem>();
                })

                .Concat(SubjectLines.SelectMany(subjectLine =>
                {
                    filter = $"@SQL=urn:schemas:httpmail:subject LIKE '%{subjectLine}%'";
                    return usersOutlookInbox.Items.Restrict(filter).OfType<MailItem>();
                }))

                .Where(mail => mail != null && uniqueEmailIds.Add(mail.EntryID))
                .ToList();
        }
        public MAPIFolder FindOrCreate(MAPIFolder usersOutlookInbox, string folderName)
        {
            MAPIFolder folder = null;

            try
            {
                folder = usersOutlookInbox.Folders[folderName];
            }
            catch (System.Exception ex)
            {
                WriteLine($"[!] An error occurred while trying to find folder: {folderName} \n" + ex.Message);
            }


            // if the folder doesn't exist yet 
            if (folder != null)
            {
                try
                {
                    folder = usersOutlookInbox.Folders.Add(folderName);
                }
                catch (System.Exception ex)
                {
                    WriteLine($"[!] An error occurred while trying to create folder: {folderName} \n" + ex.Message);
                }
            }
            return folder;
        }   

        public void DisplayEmailsMoved()
        {
            if(FoundEmails.Count == 0)
            {
                WriteLine("[!] Cannot display emails moved because none were found [!]");
                return;
            }
            FoundEmails.ForEach( email =>
            {
                WriteLine("*".PadRight(50, '*'));
                WriteLine($"[i] An Email with the subject line => '{email.Subject}'{FolderName[0]} \n");
                WriteLine($"[i] Sender was => '{email.SenderName}' or {email.SenderEmailAddress} \n");
                WriteLine("*".PadRight(50, '*'));
            });
        }
    }
    public class Preset
    {
        public string FileName { get; set; }
        

        // Constructor when we are opening a preset
        public Preset(string filePath)
        {
            this.FileName = Path.GetFileName(filePath);
        }
        public void OpenPresetJSON(string filePath)
        {
            if(!File.Exists(filePath))
            {
                WriteLine($"[!] File does not exist at {filePath} [!]");
                return;
            }
            try
            {
                List<Filter> openFilters;
                using (StreamReader stream = File.OpenText(filePath))
                {
                    List<Filter> openFilters = JsonConvert.DeserializeObject<List<Filter>>(stream);
                }
                
            }
        }
        


    }
    

}
