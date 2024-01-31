using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace OutlookSort
{
    public static class EmailValidator
    {
        private const string emailRegexStr = @"
        ^                           # Start of string
        [a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+  # Username part
        @                           # Literal '@'
        [a-zA-Z0-9-]+               # Domain name part
        (\.[a-zA-Z0-9-]+)*          # Domain sub-parts
        \.[a-zA-Z]{2,}              # TLD
        $                           # End of string
        ";

        private static Regex emailRegex = new Regex(emailRegexStr, RegexOptions.IgnorePatternWhitespace);
        private static bool DomainExists(string domain)
        {
            try
            {
                // Check if the domain has a DNS entry (MX or A record, all emails that are valid have one)
                return Dns.GetHostAddresses(domain).Length > 0 || Dns.GetHostEntry(domain).AddressList.Length > 0;
            }
            catch (SocketException)
            {
                return false;
            }
        }
        public static bool ValidateEmail(string anEmail) => 
            !(emailRegex.IsMatch(anEmail)) || !(DomainExists(anEmail.Split('@')[1]));
        
    }

}