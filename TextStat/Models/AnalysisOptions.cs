using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextStat.Models
{
    public class AnalysisOptions
    {
        /// <summary>Количество топ слов для выдачи.</summary>
        public int TopN { get; set; } = 10;

        /// <summary>Слова в минуту для оценки чтения (wpm).</summary>
        public int ReadingWpm { get; set; } = 200;

        /// <summary>Список расширений файлов для анализа (например, ".txt", ".md"). Если null или empty — используется ".txt".</summary>
        public string[]? FileExtensions { get; set; } = new[] { ".txt" };

        /// <summary>Рекурсивно обходить директории.</summary>
        public bool Recursive { get; set; } = false;

        /// <summary>Минимальная длина слова для учёта во frequency (фильтр очень коротких слов).</summary>
        public int MinWordLengthForTop { get; set; } = 3;
    }

}
