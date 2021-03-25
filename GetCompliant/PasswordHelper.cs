using System;
using System.IO;
using System.Linq;

namespace GetCompliant
{
    public class PasswordHelper
    {
        static int compliantCount = 0;
        static int infileCount = 0;
        static string password;

        public static void ProcessFile()
        {
            Console.WriteLine("[+] Analysing passwords from {0}", Globals.infile);
            StreamWriter outFile = new StreamWriter(Globals.outfile);
            StreamReader inFile = new StreamReader(Globals.infile);
            
            while ((password = inFile.ReadLine()) != null)
            {
               infileCount++;
               if (iscompliant(password))
                {
                    compliantCount++;
                    outFile.WriteLine(password);
                }
            }
            float percent = compliantCount * 100  / infileCount;
            Console.WriteLine("[+] Extracted {0} compliant passwords from {1}, hitrate={2}%", compliantCount, infileCount, percent);
            outFile.Close();
            inFile.Close();
         }

        public static Boolean iscompliant(string password)
        {
            int passes = 0;
            if (Globals.minLength>0 && password.Length<Globals.minLength)
            {
                return false;
            }
            if (Globals.maxLength>0 && password.Length>Globals.maxLength)
            {
                return false;
            }
            if (Globals.upper && password.ToLower()!=password)
            {
                passes++;
            }
            if (Globals.lower && password.ToUpper()!=password)
            {
                passes++;
            }
            if (Globals.number && password.Any(char.IsDigit))
            {
                passes++;
            }
            if (Globals.special && ContainsSpecial(password))
            {
                passes++;
            }
            if (passes < Globals.mandatorySets)
            {
                return false;
            }
            return true;
        }

        public static bool ContainsSpecial(string p)
        {
            foreach (var c in p)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
