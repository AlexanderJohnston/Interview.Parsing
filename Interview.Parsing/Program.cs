using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace Interview.Parsing
{
    public class Program
    {
        internal static string[] _args;

        static void Main(string[] args)
        {
            _args = args;
            if (args.Length == 0)
            {
                DisplayHelp();
                return;
            }
            var parsedArguments = new ArgsParser(args);
            var outputEnabled = !string.IsNullOrEmpty(parsedArguments.OutputFileName);
            if (outputEnabled)
                ExecuteAndLogToFile(parsedArguments);
            else
                ExecuteInteractively(parsedArguments);
        }

        public static void ExecuteInteractively(ArgsParser arguments)
        {
            var analyzer = new NGramAnalyzer(2);
            analyzer.AnalyzeInputs(arguments);
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
        public static void ExecuteAndLogToFile(ArgsParser arguments)
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

                //  Restore the original console output.
                Console.SetOut(consoleOutput);
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine(@"Welcome to the Bigram Frequency Analyzer.
This program was built by Alexander Johnston and submitted 2020-28-10.

PURPOSE:
---
This program takes a phrase and tokenizes it into bigrams and then creates a simple histogram to show token frequency.
It supports symbols in the middle of bigrams, such as ""Price $100.00"".
Punctuation and grammar will be trimmed automatically by the program when it creates tokens.

HOW TO USE:
---
This program runs in three different modes, ""Single"", ""Multi"", and ""File"".

[Single Mode]:
EX: Interview.Parsing.exe ""Input Phrase Input Phrase""
This mode will parse a single phrase and display the output in the console or stdout.

[Multi Mode]:
EX: Interview.Parsing.exe -multi ""One Bigram"" ""Two Bigram Two Bigram""
Multi-mode will parse as many outputs as you can fit into separate arguments and output to the console.
The -multi switch is required.

[File Mode]:
EX: Interview.Parsing.exe ""input.txt""
EX: Itnerview.Parsing.exe ""input.txt"" ""output.txt""
The first argument should contain a valid path to your input phrases as a plaintext file. Any number of phrases
can be added to the file. Phrases separated by newlines will be procesed separately.
The second argument specifies an output file, otherwise the program will output to the console.
");
        }
    }
}
