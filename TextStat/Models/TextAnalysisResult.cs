using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextStat.Models
{
    public class TextAnalysisResult
    {
        public string FilePath { get; init; } = "";
        public long CharCount { get; init; }
        public long WordCount { get; init; }
        public long SentenceCount { get; init; }
        public long ParagraphCount { get; init; }
        public double AverageWordLength { get; init; }
        public double AverageSentenceLengthWords { get; init; }
        public double EstimatedReadingMinutes { get; init; }
        public IReadOnlyList<(string Word, int Count)> TopWords { get; init; } = new List<(string, int)>();
    }
}
