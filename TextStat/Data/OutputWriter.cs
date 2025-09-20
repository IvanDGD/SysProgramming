using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextStat.Models;

namespace TextStat.Data
{
    /// <summary>
    /// Утилиты для вывода результата: консоль, JSON, CSV.
    /// </summary>
    public static class OutputWriter
    {
        /// <summary>Вывести результат одного анализа в консоль (читабельно).</summary>
        public static void PrintToConsole(TextAnalysisResult r)
        {
            Console.WriteLine($"File: {r.FilePath}");
            Console.WriteLine($"Chars: {r.CharCount}, Words: {r.WordCount}, Sentences: {r.SentenceCount}, Paragraphs: {r.ParagraphCount}");
            Console.WriteLine($"Avg word len: {r.AverageWordLength}, Avg sentence (words): {r.AverageSentenceLengthWords}");
            Console.WriteLine($"Est. reading minutes: {r.EstimatedReadingMinutes}");
            Console.WriteLine("Top words:");
            foreach (var (w, c) in r.TopWords)
            {
                Console.WriteLine($"  {w} — {c}");
            }
        }

        /// <summary>Сериализовать набор результатов в JSON и записать в файл.</summary>
        public static void WriteJson(IEnumerable<TextAnalysisResult> results, string path)
        {
            var opts = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(results, opts);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        /// <summary>Записать набор результатов в CSV (простая схема).</summary>
        public static void WriteCsv(IEnumerable<TextAnalysisResult> results, string path)
        {
            using var sw = new StreamWriter(path, false, Encoding.UTF8);
            sw.WriteLine("FilePath,Chars,Words,Sentences,Paragraphs,AvgWordLen,AvgSentenceWords,ReadMinutes,TopWords");
            foreach (var r in results)
            {
                var top = string.Join(";", r.TopWords.Select(t => $"{EscapeCsv(t.Word)}:{t.Count}"));
                sw.WriteLine($"{EscapeCsv(r.FilePath)},{r.CharCount},{r.WordCount},{r.SentenceCount},{r.ParagraphCount},{r.AverageWordLength},{r.AverageSentenceLengthWords},{r.EstimatedReadingMinutes},{EscapeCsv(top)}");
            }
        }

        private static string EscapeCsv(string s)
        {
            if (s is null) return "";
            if (s.Contains('"') || s.Contains(',') || s.Contains('\n') || s.Contains('\r'))
            {
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }
    }
}
