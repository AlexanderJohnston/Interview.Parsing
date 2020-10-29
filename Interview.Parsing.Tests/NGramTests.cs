using System;
using System.Collections.Generic;
using Xunit;

namespace Interview.Parsing.Tests
{
    public class NGramTests
    {
        private Dictionary<string, int> CountFrequency(IEnumerable<string> nGrams)
        {
            var frequency = new Dictionary<string, int>();
            foreach (var nGram in nGrams)
            {
                if (frequency.ContainsKey(nGram.ToLower()))
                {
                    frequency[nGram.ToLower()] += 1;
                }
                else
                {
                    frequency.Add(nGram.ToLower(), 1);
                }
            }
            return frequency;
        }
        [Fact]
        public void CreateBasicPhrase()
        {
            var phrase = "One token two token two token three token three token three token";
            var creator = new NGramCreator();
            var nGrams = creator.ParseTokens(phrase, 2);
            var frequency = CountFrequency(nGrams);
            Assert.True(frequency["one token"] == 1);
            Assert.True(frequency["two token"] == 2);
            Assert.True(frequency["token two"] == 2);
            Assert.True(frequency["three token"] == 3);
            Assert.True(frequency["token three"] == 3);
        }

        [Fact]
        public void CreateBigram()
        {
            var phrase = "A bigram.";
            var creator = new NGramCreator();
            var nGrams = creator.ParseTokens(phrase, 2);
            var frequency = CountFrequency(nGrams);
            Assert.True(frequency["a bigram"] == 1 && frequency.Count == 1);
        }

        [Fact]
        public void CreateBigramWithSymbols()
        {
            var phrase = "192.168.0.1 DISCONNECTED";
            var creator = new NGramCreator();
            var nGrams = creator.ParseTokens(phrase, 2);
            var frequency = CountFrequency(nGrams);
            Assert.True(frequency["192.168.0.1 disconnected"] == 1 && frequency.Count == 1);
        }

        [Fact]
        public void CreateTrigram()
        {
            var phrase = "Neat interview sample?";
            var creator = new NGramCreator();
            var nGrams = creator.ParseTokens(phrase, 3);
            var frequency = CountFrequency(nGrams);
            Assert.True(frequency["neat interview sample"] == 1 && frequency.Count == 1);
        }
    }
}
