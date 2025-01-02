using Telegram.Bot.Exceptions;
using Telegram.Bot;
using System.Data.SQLite;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using tgbot_LAPTOP_SHOP.KeyboardInteraction;
using tgbot_LAPTOP_SHOP.GoodsSelecting;

namespace tgbot_LAPTOP_SHOP
{
    static class BotLogic
    {
        public static async Task HandleUpdateAsync(ITelegramBotClient telegramBotClient, Update update)
        {
            try
            {
                SQLiteConnection connection = DatabaseSetup.SQLite_connection();
                SQLiteCommand command = connection.CreateCommand();

                switch (update.Type)
                {
                    case UpdateType.Message:
                        if(update.Message != null)
                            await HandleMessageAsync(telegramBotClient, update.Message);
                        break;

                    case UpdateType.CallbackQuery:
                        if (update.CallbackQuery != null)
                            await HandleCallbackQueryAsync(telegramBotClient, update.CallbackQuery, command);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task HandleMessageAsync(ITelegramBotClient telegramBotClient, Message message)
        {
            var chatId = message.Chat.Id;
            var messageText = message.Text?.ToLower();

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            if (message.Text == "/start")
            {
                await telegramBotClient.SendTextMessageAsync(message.Chat, "Вітаємо в нашому магазині!");
                var inlineKeyboard = KeyboardMain.GetKeyboardMain();
                await telegramBotClient.SendTextMessageAsync(chatId, "Що ви бажаєте придбати?", replyMarkup: inlineKeyboard);
            }
            else if (message.Text == "Повернутись до стартової сторінки")
            {
                var inlineKeyboard = KeyboardMain.GetKeyboardMain();
                await telegramBotClient.SendTextMessageAsync(chatId, "Що ви бажаєте придбати?", replyMarkup: inlineKeyboard);
            }
            else if (message.Text == ("Повернутись до фільтрів"))
            {
                var inlineKeyboard = KeyboardFilters.GetMainFilters();
                await telegramBotClient.SendTextMessageAsync(chatId, "Що ви бажаєте придбати?", replyMarkup: inlineKeyboard);
            }
            else
                await telegramBotClient.SendTextMessageAsync(chatId, "Оберіть дію з запропонованих на кнопках!");
        }

        public static async Task HandleCallbackQueryAsync(ITelegramBotClient telegramBotClient, CallbackQuery callbackQuery, SQLiteCommand command)
        {
            if (callbackQuery.Message != null)
            {
                var chatId = callbackQuery.Message.Chat.Id;
                await Filtering.SelectAllFilters(telegramBotClient, callbackQuery, command);
                var replyKeyboard = KeyboardReturn.GetReturnKeyboard();
                await telegramBotClient.SendTextMessageAsync(chatId, "Для повернення натисніть кнопки внизу", replyMarkup: replyKeyboard);
            }
            else
            {
                Console.WriteLine("CallbackQuery does not contain a message.");
            }
        }

        public static Task HandleError(Exception exception)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
