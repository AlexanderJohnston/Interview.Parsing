using System;
using System.Collections.Generic;
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
    }
}
