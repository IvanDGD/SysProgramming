using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    static ITelegramBotClient bot;
    static Dictionary<long, int> userProgress = new();
    static Dictionary<long, int> userScore = new();

    static (string Question, string[] Options, int CorrectIndex, string Explanation)[] quiz = new[]
    {
        ("До якого періоду належить творчість Леонардо да Вінчі?", new[] { "Бароко", "Ренесанс", "Імпресіонізм", "Модерн" }, 1, "Леонардо да Вінчі — ключова постать епохи Відродження."),
        ("Який художник вважається представником імпресіонізму?", new[] { "Моне", "Далі", "Мікеланджело", "Ван Гог" }, 0, "Клод Моне — засновник імпресіонізму."),
        ("Що характерно для модернізму?", new[] { "Строгі форми", "Символізм і експерименти", "Реалізм", "Релігійні сюжети" }, 1, "Модернізм відкидає класику та прагне до нових форм вираження."),
        ("Який стиль характеризується великою кількістю орнаментів і плавних ліній?", new[] { "Бароко", "Модерн", "Імпресіонізм", "Реалізм" }, 1, "Модерн вирізняється вигнутими лініями та рослинними мотивами."),
        ("Хто автор картини 'Зоряна ніч'?", new[] { "Ван Гог", "Моне", "Пікассо", "Дега" }, 0, "Вінсент Ван Гог написав 'Зоряну ніч' у 1889 році."),
        ("Який напрям прагнув передати враження від побаченого, а не точну копію?", new[] { "Імпресіонізм", "Кубізм", "Реалізм", "Романтизм" }, 0, "Імпресіоністи писали світло й рух, а не деталі."),
        ("Хто є одним із засновників кубізму?", new[] { "Пікассо", "Рембрандт", "Мікеланджело", "Сальвадор Далі" }, 0, "Пабло Пікассо — один із творців кубізму."),
        ("Який художник відомий своїми сюрреалістичними картинами з годинниками?", new[] { "Далі", "Моне", "Ренуар", "Гоген" }, 0, "Сальвадор Далі створив знамениту картину 'Плинність часу'."),
        ("Що характерно для бароко?", new[] { "Динаміка та пишність", "Простота і строгість", "Абстрактні форми", "Мінімалізм" }, 0, "Бароко вирізняється емоційністю та багатим декором."),
        ("Хто написав фреску 'Створення Адама'?", new[] { "Леонардо да Вінчі", "Рафаель", "Мікеланджело", "Боттічеллі" }, 2, "Фреска 'Створення Адама' є частиною розпису Сікстинської капели, створеного Мікеланджело."),
    };


    static async Task Main()
    {
        string botApi = "8409894811:AAHPWvQ70HlTrsMD-1sAwuEUw1yfoqhU4TI";
        bot = new TelegramBotClient(botApi);

        Console.WriteLine("Bot started: @HfhvdfjdBot");

        var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);

        Console.ReadLine();
        cts.Cancel();
    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception ex, CancellationToken token)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
        return Task.CompletedTask;
    }

    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        if (update.Type == UpdateType.Message && update.Message?.Text != null)
        {
            await HandleMessage(update.Message);
        }
        else if (update.Type == UpdateType.CallbackQuery)
        {
            await HandleCallback(update.CallbackQuery!);
        }
    }

    private static async Task HandleMessage(Message msg)
    {
        long userId = msg.Chat.Id;
        string text = msg.Text!.ToLower();

        if (text == "/start")
        {
            userProgress[userId] = 0;
            userScore[userId] = 0;
            await SendQuestion(userId);
            return;
        }

        await bot.SendMessage(userId, "Send /start");
    }

    private static async Task HandleCallback(CallbackQuery query)
    {
        long userId = query.From.Id;
        if (!userProgress.ContainsKey(userId)) return;

        int index = userProgress[userId];
        var (question, options, correct, explanation) = quiz[index];

        int chosen = int.Parse(query.Data!);
        bool isCorrect = chosen == correct;

        if (isCorrect)
        {
            userScore[userId]++;
            await bot.SendMessage(userId, "Correct");
        }
        else
        {
            await bot.SendMessage(userId, $"Wrong");
        }

        userProgress[userId]++;

        if (userProgress[userId] < quiz.Length)
        {
            await SendQuestion(userId);
        }
        else
        {
            await bot.SendMessage(userId, $"Quiz is end, your results: {userScore[userId]}/{quiz.Length}");
            userProgress.Remove(userId);
            userScore.Remove(userId);
        }
    }

    private static async Task SendQuestion(long chatId)
    {
        int index = userProgress[chatId];
        var (question, options, _, _) = quiz[index];

        var buttons = new List<List<InlineKeyboardButton>>();
        for (int i = 0; i < options.Length; i++)
            buttons.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(options[i], i.ToString()) });

        var markup = new InlineKeyboardMarkup(buttons);
        await bot.SendMessage(chatId, $"❓ {question}", replyMarkup: markup);
    }
}
