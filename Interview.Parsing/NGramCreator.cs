using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Parsing
{
    public class NGramCreator
    {
        private Queue<int> _GramBuffer = new Queue<int>();

        public IEnumerable<string> ParseTokens(string phrase, int tokenSize)
        {
            var creator = new StringBuilder();
            // Span<T> cannot be boxed from the Stack so we need Memory<T> in order to yield Enumerables from the Heap.
            var memory = phrase.AsMemory();
            // Tracks the word length for the sake of clearing stringbuilder memory based on index and length
            var nGramLength = 0;
            var tokensFound = 0;

            // This saves us from a lot of boundary checks for the first character by doing it outside the loop
            if (char.IsLetterOrDigit(memory.Span[0]))
            {
                creator.Append(memory.Span[0]);
                nGramLength = 1;
            }
            for (int i = 1; i < memory.Span.Length; i++)
            {
                var current = memory.Span[i];
                var last = memory.Span[i - 1];

                // If we've hit the last character then we can skip the entire process and just yield.
                if(i != memory.Span.Length - 1)
                {
                    var next = memory.Span[i + 1];
                    if (char.IsLetterOrDigit(current) || ValidTokenSymbol(current, last, next))
                    {
                        creator.Append(current);
                        nGramLength += 1;
                    }
                    else
                    {
                        if (nGramLength > 0)
                        {
                            _GramBuffer.Enqueue(nGramLength);
                            nGramLength = 0;
                            tokensFound++;

                            // Check to ensure the buffer has grown large enough to begin emitting n-grams
                            if (tokensFound >= tokenSize)
                            {
                                yield return creator.ToString();
                                var lostBufferSize = _GramBuffer.Dequeue() + 1;
                                creator.Remove(0, lostBufferSize);
                                tokensFound -= 1;
                            }
                            creator.Append(" ");
                        }
                    }
                }
                else
                {
                    if (char.IsLetterOrDigit(current))
                    {
                        creator.Append(current);
                        nGramLength += 1;
                    }
                    _GramBuffer.Enqueue(nGramLength);
                    yield return creator.ToString();
                }
                
            }
        }

        /// <summary>
        /// Checks to ensure that any punctuation or symbols are contained within a token and not just grammatical.
        /// </summary>
        /// <param name="symbol">The symbol to test</param>
        /// <param name="before">The character preceding the symbol</param>
        /// <param name="after">The character immediately following the symbol</param>
        private bool ValidTokenSymbol(char symbol, char before, char after)
        {
            return !char.IsWhiteSpace(symbol)
              && (char.IsPunctuation(symbol) || char.IsSeparator(symbol) || char.IsSymbol(symbol))
              && (char.IsLetterOrDigit(before) || char.IsWhiteSpace(before)) 
              && char.IsLetterOrDigit(after);
        }
    }
}
