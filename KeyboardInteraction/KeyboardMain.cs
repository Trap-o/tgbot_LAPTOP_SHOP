using Telegram.Bot.Types.ReplyMarkups;

namespace tgbot_LAPTOP_SHOP.KeyboardInteraction
{
    static internal class KeyboardMain
    {
        public static InlineKeyboardMarkup GetKeyboardMain()
        {
            var inlineKeyboardMarkup = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Ноутбуки", "laptopButton"),
                        InlineKeyboardButton.WithCallbackData("Інше", "othersButton")
                    }
                }
            );

            return inlineKeyboardMarkup;
        }
    }
}
