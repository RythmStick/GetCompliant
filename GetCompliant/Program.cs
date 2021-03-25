using NDesk.Options;
using System;
using System.IO;

namespace GetCompliant
{
    using static Globals;
    public static class Globals
    {
        public static string infile;
        public static string outfile;
        public static int minLength=0;
        public static int maxLength=0;
        public static Boolean force;
        public static Boolean upper;
        public static Boolean lower;
        public static Boolean number;
        public static Boolean special;
        public static int mandatorySets=0;
        public static Boolean help;
        public static string setsverbose;
        public static int setsSpecified = 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (!validParameters(args))
            {
                return;
            }
            SummarizePolicy();
            PasswordHelper.ProcessFile();
        }

        public static void SummarizePolicy()
        {
            Console.WriteLine("[+] Extracting passwords that meet the following complexity rules:");
            if (setsSpecified > 0)
            {
                Console.WriteLine("    Must include {0} of the following sets: {1}", mandatorySets, setsverbose);
            }
            if (minLength > 0)
            {
                Console.WriteLine("    Minimum Length is {0}", minLength);
            }
            if (maxLength > 0)
            {
                Console.WriteLine("    Maximum Length is {0}", maxLength);
            }
        }

        public static Boolean validParameters(string[] args)
        {
            var options = new OptionSet(){
                {"i|inputfile=", "Password File eg. rockyou.txt", o => infile = o},
                {"o|outputfile=", "Output File", o => outfile = o},
                {"f|forceoverwrite","force overwrite if Output file exists", o => force = true},
                {"m|minlength=", "Minimum password Length", (int o) => minLength = o},
                {"x|maxlength=", "Maximum password Length", (int o) => maxLength = o},
                {"u|upper","must include Uppercase set", o => upper = true},
                {"l|lower","must include Lowercase set", o => lower = true},
                {"p|special","must include Special Char set", o => special = true},
                {"n|number","must include Number set", o => number = true},
                {"s|sets=","number of Mandatory sets (default: All Sets)", (int o) => mandatorySets = o},
                {"h|?|help","Show Help", o => help = true},
            };
            try
            {
                options.Parse(args);
                if (help || args.Length == 0)
                {
                    showHelp(options);
                    return false;
                }
                if (infile==null)
                {
                    Console.WriteLine("[-] Must specify an input file using -i");
                    return false;
                }
                if (!File.Exists(infile))
                {
                    Console.WriteLine("[-] File {0} not found", infile);
                    return false;
                }
                if (outfile == null)
                {
                    Console.WriteLine("[-] Must specify an output file using -o");
                    return false;
                }
                if ((!File.Exists(outfile)))
                {
                    FileStream fs = File.Create(outfile); 
                    fs.Close();
                }
                else if (!force)
                {
                    Console.WriteLine("[-] Output File already exists, use -f to force overwrite");
                    return false;
                }
                if (upper)  { setsSpecified++; setsverbose += "Uppercase "; }
                if (lower)  { setsSpecified++; setsverbose += "Lowercase "; }
                if (special) { setsSpecified++; setsverbose += "Special "; }
                if (number) { setsSpecified++; setsverbose += "Numbers "; } 
                if (mandatorySets > setsSpecified)
                {
                    Console.WriteLine("[-] Mandatory Sets -s ({0}) must not be > sets supplied ({1})", mandatorySets, setsSpecified);
                    return false;
                }
                if (mandatorySets==0)
                {
                    mandatorySets = setsSpecified;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                showHelp(options);
                return false;
            }
            return true;
        }

        public static void showHelp(OptionSet p)
        {
            Console.WriteLine(@"           _                          _  _            _   ");
            Console.WriteLine(@" ___  ___ | |_  ___  ___  _____  ___ | ||_| ___  ___ | |_");
            Console.WriteLine(@"| . || -_||  _||  _|| . ||     || . || || || .'||   ||  _|");
            Console.WriteLine(@"| _ ||___||_|  |___||___||_|_|_||  _||_||_||__,||_|_||_|");
            Console.WriteLine(@"|___|                           |_|                       ");
            Console.WriteLine("@_RythmStick\n\n\n");
            Console.WriteLine("Compliant password Extractor\nUsage:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}

