using System;
using System.Collections.Generic;
using System.IO;
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
            var parsedArguments = new ArgsParser(args);
            var outputEnabled = string.IsNullOrEmpty(parsedArguments.OutputFileName);
            if (outputEnabled)
                ExecuteAndLogToFile(parsedArguments);
            else
                ExecuteInteractively(parsedArguments);
        }

        private static void ExecuteInteractively(ArgsParser arguments)
        {
            var analyzer = new NGramAnalyzer(2);
            analyzer.AnalyzeInputs(arguments);
            Console.WriteLine("Exit?");
            Console.ReadLine();
        }

        /// <summary>
        ///     Outputs the <see cref="Console" /> to a file located in the <see cref="AppDomain" /> base directory.
        /// </summary>
        /// <param name="rover">A <see cref="LandRover" /> to be moved.</param>
        /// <param name="movementInstructions">A list of instructions to be passed to the <see cref="LandRover" />.</param>
        /// <param name="fileName">
        ///     The name of the file containing output strings which report the <see cref="LandRover" /> current
        ///     position after each instruction.
        /// </param>
        private static void ExecuteAndLogToFile(ArgsParser arguments)
        {
            FileStream file;
            if (File.Exists(arguments.OutputFileName))
                file = new FileStream($"{arguments.OutputFileName}", FileMode.Append);
            else
                file = new FileStream($"{arguments.OutputFileName}", FileMode.OpenOrCreate);
            var consoleOutput = Console.Out;
            using (var sWriter = new StreamWriter(file))
            {
                //  Trace the console out to the file.
                Console.SetOut(sWriter);
                var analyzer = new NGramAnalyzer(2);
                analyzer.AnalyzeInputs(arguments);
                Console.WriteLine("Exit?");
                Console.ReadLine();

                //  Restore the original console output.
                Console.SetOut(consoleOutput);
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
