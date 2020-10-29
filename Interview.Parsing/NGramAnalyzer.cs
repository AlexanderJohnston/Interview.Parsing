using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace Interview.Parsing
{
    public class NGramAnalyzer
    {
        public Dictionary<string, int> FrequencyTable = new Dictionary<string, int>();
        public int NGramSize = 2;

        public NGramAnalyzer(int nGramSize)
        {
            NGramSize = nGramSize;
        }

        public void AnalyzeInputs(ArgsParser parsed)
        {
            FrequencyTable = new Dictionary<string, int>();
            foreach (var input in parsed.Inputs)
            {
                var creator = new NGramCreator();
                var FrequencyTable = new Dictionary<string, int>();
                foreach (var nGram in creator.ParseTokens(input, NGramSize))
                {
                    if (FrequencyTable.ContainsKey(nGram.ToLower()))
                    {
                        FrequencyTable[nGram.ToLower()] += 1;
                    }
                    else
                    {
                        FrequencyTable.Add(nGram.ToLower(), 1);
                    }
                }
                DisplayCurrentAnalysis(FrequencyTable, input);
            }
        }

        public void DisplayCurrentAnalysis(Dictionary<string, int> frequencyTable, string phrase)
        {
            if (frequencyTable == null || frequencyTable.Count == 0)
            {
                return;
            };
            Console.WriteLine("BiGram Analysis For:");
            if (phrase.Length > 100)
            {
                Console.WriteLine($"{phrase.Substring(0, 100)} ...[cutoff]");
            }
            else
            {
                Console.WriteLine(phrase);
            }
            foreach (var nGram in frequencyTable)
            {
                Console.WriteLine($"\"{nGram.Key}\": {nGram.Value}");
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
