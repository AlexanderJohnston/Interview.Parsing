using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Interview.Parsing.Tests
{
    public class ArgsTests
    {
        [Fact]
        public void TestSingularInput()
        {
            var args = new[] { "192.168.0.1", "CONNECTED", "192.168.0.1", "CONNECTED", "192.168.0.1", "DISCONNECTED" };
            var parser = new ArgsParser(args);
            var inputs = parser.Inputs;
            Assert.True(inputs.Count == 1 && inputs[0] == "192.168.0.1 CONNECTED 192.168.0.1 CONNECTED 192.168.0.1 DISCONNECTED");
        }

        [Fact]
        public void TestMultipleInput()
        {
            var args = new[] { "-multi", "One token two token two token", "192.168.0.1 LOG-IN_NOW 192.168.0.1 LOG-OUT_NOW"};
            var parser = new ArgsParser(args);
            var inputs = parser.Inputs;
            Assert.True(inputs.Count == 2);
            Assert.True(inputs[0] == "One token two token two token");
            Assert.True(inputs[1] == "192.168.0.1 LOG-IN_NOW 192.168.0.1 LOG-OUT_NOW");
        }

        [Fact]
        public void TestOutputFile()
        {
            var test = Assembly.GetExecutingAssembly().CodeBase;
            var test2 = test.Substring(8, test.Length - 8);
            var fileInfo = new FileInfo(test2);
            var directory = fileInfo.Directory.FullName;
            var inputSample = $"{directory}\\Samples\\hundred-bigrams-with-symbols.txt";
            var args = new[] { inputSample, ".\\sample-output.txt" };
            var parser = new ArgsParser(args);
            var inputFile = parser.InputFileName;
            var outputFile = parser.OutputFileName;
            Program.ExecuteAndLogToFile(parser);
            Assert.True(inputFile != null && File.Exists(inputFile));
            Assert.True(outputFile != null && File.Exists(outputFile)); 
        }
    }
}
