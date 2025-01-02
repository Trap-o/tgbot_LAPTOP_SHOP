using Telegram.Bot.Types.ReplyMarkups;

namespace tgbot_LAPTOP_SHOP.KeyboardInteraction
{
    static internal class KeyboardReturn
    {
        public static ReplyKeyboardMarkup GetReturnKeyboard()
        {
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>()
                {
                    new KeyboardButton[]
                    {
                        new("Повернутись до стартової сторінки"),
                    },
                    new KeyboardButton[]
                    {
                        new("Повернутись до фільтрів")
                    }
                }
            )
            { ResizeKeyboard = true };

            return replyKeyboardMarkup;
        }
    }
}
