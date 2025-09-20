using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextStat.Models;

namespace TextStat.Data
{
    public static class TextAnalyzer
    {
        // Базовый (неполный) список стоп-слов — можно расширять.
        private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "the","and","a","an","of","in","to","is","it","that","this","for","on","with","as","are","was","were","be","by","or","from","at","which","I","you","he","she","we","they"
    };

        // Слова: буква + буквы/маркеры (включая диакритику)
        private static readonly Regex WordRegex = new(@"\p{L}[\p{L}\p{M}'’-]*", RegexOptions.Compiled);
        // Простейшее разделение предложений (на границе . ! ? + пробел/конец)
        private static readonly Regex SentenceSplit = new(@"[.!?]+(?=\s|$)", RegexOptions.Compiled);
        // Разделение абзацев — двойной перевод строки
        private static readonly Regex ParagraphSplit = new(@"\r\n\r\n|\n\n", RegexOptions.Compiled);

        /// <summary>
        /// Проанализировать текст и вернуть результат анализа.
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="filePath">Опциональный путь файла (для результата)</param>
        /// <param name="options">Опции анализа</param>
        public static TextAnalysisResult AnalyzeText(string text, string? filePath = null, AnalysisOptions? options = null)
        {
            options ??= new AnalysisOptions();
            text ??= string.Empty;

            var charCount = text.Length;

            var paragraphs = ParagraphSplit.Split(text).Count(s => !string.IsNullOrWhiteSpace(s));

            var wordMatches = WordRegex.Matches(text);
            var words = wordMatches.Select(m => m.Value).ToList();
            var wordCount = words.Count;

            var sentenceParts = SentenceSplit.Split(text).Count(s => !string.IsNullOrWhiteSpace(s));
            var sentenceCount = sentenceParts == 0 && wordCount > 0 ? 1 : sentenceParts;

            double avgWordLength = wordCount > 0 ? Math.Round(words.Average(w => w.Length), 2) : 0;
            double avgSentenceWords = sentenceCount > 0 ? Math.Round((double)wordCount / sentenceCount, 2) : 0;
            double readingMinutes = Math.Round((double)wordCount / Math.Max(1, options.ReadingWpm), 2);

            // частота слов (нормализуем в lower)
            var freq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var w in words)
            {
                var wNorm = w.Trim().ToLowerInvariant();
                if (wNorm.Length < options.MinWordLengthForTop) continue;
                if (StopWords.Contains(wNorm)) continue;
                if (freq.ContainsKey(wNorm)) freq[wNorm]++; else freq[wNorm] = 1;
            }

            var top = freq.OrderByDescending(kv => kv.Value)
                            .ThenBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
                            .Take(options.TopN)
                            .Select(kv => (kv.Key, kv.Value))
                            .ToList();

            return new TextAnalysisResult
            {
                FilePath = filePath ?? string.Empty,
                CharCount = charCount,
                WordCount = wordCount,
                SentenceCount = sentenceCount,
                ParagraphCount = paragraphs,
                AverageWordLength = avgWordLength,
                AverageSentenceLengthWords = avgSentenceWords,
                EstimatedReadingMinutes = readingMinutes,
                TopWords = top
            };
        }

        /// <summary>
        /// Проанализировать содержимое файла.
        /// </summary>
        public static TextAnalysisResult AnalyzeFile(string filePath, AnalysisOptions? options = null)
        {
            if (filePath is null) throw new ArgumentNullException(nameof(filePath));
            var text = System.IO.File.ReadAllText(filePath);
            return AnalyzeText(text, filePath, options);
        }

        /// <summary>
        /// Проанализировать все файлы в директории (по расширениям options.FileExtensions).
        /// Возвращает перечисление результатов (в порядке обхода).
        /// </summary>
        public static IEnumerable<TextAnalysisResult> AnalyzeDirectory(string directory, AnalysisOptions? options = null)
        {
            if (directory is null) throw new ArgumentNullException(nameof(directory));
            if (!System.IO.Directory.Exists(directory)) throw new System.IO.DirectoryNotFoundException(directory);

            options ??= new AnalysisOptions();
            var exts = (options.FileExtensions == null || options.FileExtensions.Length == 0)
                ? new[] { ".txt" }
                : options.FileExtensions;

            var searchOpt = options.Recursive ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly;

            var files = exts.SelectMany(ext =>
                System.IO.Directory.EnumerateFiles(directory, $"*{ext}", searchOpt))
                .OrderBy(p => p, StringComparer.OrdinalIgnoreCase);

            foreach (var f in files)
            {
                TextAnalysisResult? r = null;
                try
                {
                    r = AnalyzeFile(f, options);
                }
                catch
                {
                    // при ошибке чтения — возвращаем минимальный объект с FilePath и ошибочным содержимым (можно изменить логику)
                    r = new TextAnalysisResult { FilePath = f };
                }
                yield return r;
            }
        }
    }
}
