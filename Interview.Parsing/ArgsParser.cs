using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Interview.Parsing
{
    public class ArgsParser
    {
        /// <summary>
        ///     Using a stack lets us keep track of the order they
        /// </summary>
        public List<string> FilesPassedAsArgs = new List<string>();

        public List<string> Inputs = new List<string>();

        public string InputFileName;
        public string OutputFileName;

        public ArgsParser(string[] args)
        {
            // Boundary checking before testing the first two arguments
            bool inputFileProvided = false;
            for (int i = 0; i < args.Length && i < 2; i++)
            {
                if (File.Exists(args[i]))
                {
                    // If the first arg is a file path we will assume it is the input
                    inputFileProvided = i == 0 ? true : inputFileProvided;
                    FilesPassedAsArgs.Add(args[i]);
                }
            }
            if (inputFileProvided)
            {
                InputFileName = FilesPassedAsArgs[0];
                OutputFileName = args.Length > 1 ? args[1] : string.Empty;
            }
            // If there are no files passed in then we must assume the args are the input
            else
            {
                if (args[0] == "-multi")
                {
                    ReadAsMultipleInputs(args);
                }
                else
                {
                    ReadAsSingularInput(args);
                }
            }
        }

        private void ReadAsSingularInput(string[] args)
        {
            var input = string.Join(" ", args);
            Inputs.Add(input);
        }

        private void ReadAsMultipleInputs(string[] args)
        {
            // Drop the -multi switch used to declare this mode.
            var inputs = args.Skip(1).ToArray();
            Inputs.AddRange(inputs);
        }
    }
}
