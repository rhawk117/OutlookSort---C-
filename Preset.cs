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
using System.Text.RegularExpressions;


namespace OutlookSort
{
    public class Filter
    {
        public bool invalidList(List<string> list) => list.Count == 0;
        private List<string> _subjectLines;

        public List<string> SubjectLines
        {
            get => _subjectLines;
            set
            {
                if (!invalidList(value))
                    this._subjectLines = value;
                else
                    throw new ArgumentException("Cannot create a filter with an empty list of subject lines");
            }
        }
        private List<string> _emailAddresses;
        public List<string> EmailAddresses
        { 
            get => _emailAddresses;
            set
            {
                if (!invalidList(value))
                    this._emailAddresses = value;
                else
                    throw new ArgumentException("Cannot create a filter with an empty list of Email Addresses");
            }

        }
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
        public MAPIFolder FindOrCreate(MAPIFolder usersOutlookInbox)
        {
            MAPIFolder folder = null;

            try
            {
                folder = usersOutlookInbox.Folders[this.FolderName[0]];
            }
            catch (System.Exception ex)
            {
                WriteLine($"[!] An error occurred while trying to find folder: {this.FolderName} \n" + ex.Message);
            }
            // if the folder doesn't exist yet 
            if (folder != null)
            {
                try
                {
                    folder = usersOutlookInbox.Folders.Add(FolderName[0]);
                }
                catch (System.Exception ex)
                {
                    WriteLine($"[!] An error occurred while trying to create folder: {this.FolderName[0]} \n" + ex.Message);
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
        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                if (value.EndsWith("json"))
                    this._fileName = value;
                else
                    throw new ArgumentException("A Preset File must be a JSON file");
            }
        }
        private List<Filter> _presetFilters;
        public List<Filter> PresetFilters
        {
            get => _presetFilters;
            set
            {
                if(value.Count > 0)
                    this._presetFilters = value;
                else
                    throw new ArgumentException("A Preset File cannot be empty");
            }
        }

        public bool isInvalid { get => PresetFilters.Count == 0; }

        // Constructor when we are opening a preset
        public Preset(string filePath)
        {
            this.FileName = Path.GetFileName(filePath);
            this.OpenPresetJSON(filePath);
            if(isInvalid)
            {
                WriteLine("[!] The preset selected was invalid due to no Filters being found, please check the file and try again [!]");
            }
        }
        public Preset(string FileName, List<Filter> Filters)
        {
            this.FileName = FileName;
        }
        public void OpenPresetJSON(string filePath)
        {
            if(!File.Exists(filePath) || FileName.EndsWith("json"))
            {
                WriteLine($"[!] File does not exist at {filePath} [!]");
                return;
            }
            try
            {
                using (StreamReader stream = File.OpenText(filePath))
                {
                    PresetFilters = JsonConvert.DeserializeObject<List<Filter>>(stream.ReadToEnd());
                    WriteLine($"[i] Successfully opened preset at {filePath} [i]");
                }
            }
            catch(FileNotFoundException ex)
            {
                WriteLine("[!] The preset selected was not found, details on line below [!]\n");
                WriteLine(ex.Message);
                return;
            }
            catch (System.Exception ex)
            {
                WriteLine($"[!] An error occurred while trying to open preset at {filePath} \n" + ex.Message);
                return;
            }
        }
        
        

    }
    

}
