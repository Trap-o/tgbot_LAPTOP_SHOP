using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace tgbot_LAPTOP_SHOP
{
    static internal class Program
    {
        static readonly ITelegramBotClient telegramBotClient = new TelegramBotClient(PrivateData.token);

        static async Task Main()
        {
            using var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates =
                [
                    UpdateType.Message,
                    UpdateType.CallbackQuery
                ],
                ThrowPendingUpdates = true
            };

            telegramBotClient.StartReceiving(
                (client, update, token) => BotLogic.HandleUpdateAsync(client, update),
                (client, exception, token) => BotLogic.HandleError(exception),
                receiverOptions,
                cancellationToken
            );

            Console.WriteLine("Запуск бота " + telegramBotClient.GetMeAsync().Result.FirstName);
            await Task.Delay(-1, cancellationToken);
        }
    }
}