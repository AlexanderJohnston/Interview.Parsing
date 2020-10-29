using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace Interview.Parsing
{
    class Program
    {
        internal static string[] _args;

        static void Main(string[] args)
        {
            _args = args;
            if (args.Length == 0)
            {
                DisplayHelp();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                FilePathSanityCheck();
            }
            var parsedArguments = new ArgsParser(args);
            var analyzer = new NGramAnalyzer(2);
            analyzer.AnalyzeInputs(parsedArguments);

            Console.WriteLine("Exit?");
            Console.ReadLine();
        }

        

        /// <summary>
        /// Displays a help prompt on the console after checing if you have whitespace in any file paths not wrapped in quotes.
        /// 
        /// </summary>
        private static void FilePathSanityCheck()
        {
            var windowsPathRegex = new Regex(@"^\w:\\.+?\w+\s(?:$|\w+|(?:[\w|()]+)?\\)");
            var rawArguments = string.Join("", _args);
            var match = windowsPathRegex.Match(rawArguments);
            if (match.Success)
            {
                DisplayPathWarning();
            }
        }

        private static void DisplayPathWarning()
        {

        }

        private static void DisplayHelp()
        {

        }
    }
}
