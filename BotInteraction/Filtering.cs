using System.Data.SQLite;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using tgbot_LAPTOP_SHOP.KeyboardInteraction;

namespace tgbot_LAPTOP_SHOP.GoodsSelecting
{
    public static class Filtering
    {
        public static async Task SelectAllFilters(ITelegramBotClient telegramBotClient, Telegram.Bot.Types.CallbackQuery callbackQuery, SQLiteCommand command)
        {
            var chatID = callbackQuery.Message?.Chat.Id;

            switch (callbackQuery.Data)
            {
                #region MainButtons
                case "laptopButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки!");

                        var inlineKeyboard = KeyboardFilters.GetMainFilters();
                        await telegramBotClient.SendTextMessageAsync(chatID, "Оберіть фільтр", replyMarkup: inlineKeyboard);

                        return;
                    }
                case "othersButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано інші товари!");

                        command.CommandText = "SELECT * FROM Others";
                        await DatabaseSetup.SelectOthers(telegramBotClient, command, callbackQuery.Message.Chat);

                        return;
                    }
                #endregion

                #region FilterButtons
                case "brandButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано сортування за виробником!");

                        var inlineKeyboard = KeyboardFilters.GetBrands();

                        await telegramBotClient.SendTextMessageAsync(chatID, "Оберіть виробника", replyMarkup: inlineKeyboard);
                        return;
                    }
                case "priceButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано сортування за ціною!");

                        var inlineKeyboard = KeyboardFilters.GetPrices();

                        await telegramBotClient.SendTextMessageAsync(chatID, "Оберіть діапазон цін", replyMarkup: inlineKeyboard);
                        return;
                    }
                case "typeButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано сортування за типом!");

                        var inlineKeyboard = KeyboardFilters.GetTypes();

                        await telegramBotClient.SendTextMessageAsync(chatID, "Оберіть тип ноутбуків", replyMarkup: inlineKeyboard);
                        return;
                    }

                case "screenButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано сортування за розміром екрану!");

                        var inlineKeyboard = KeyboardFilters.GetScreens();

                        await telegramBotClient.SendTextMessageAsync(chatID, "Оберіть діагональ екрану", replyMarkup: inlineKeyboard);
                        return;
                    }

                case "cpuButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано сортування за процесором!");

                        var inlineKeyboard = KeyboardFilters.GetCPUs();

                        await telegramBotClient.SendTextMessageAsync(chatID, "Оберіть виробника процесора", replyMarkup: inlineKeyboard);
                        return;
                    }

                case "videocardButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки з дискретною відеокартою!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Videocard = 'Discrete'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        return;
                    }

                case "popularButton":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано популярні ноутбуки!");

                        command.CommandText = "SELECT * FROM Laptops WHERE" +
                            "(Name LIKE UPPER('%Pavilion 15%') OR " +
                            "Name LIKE UPPER('%Inspiron%') OR " +
                            "Name LIKE UPPER('%Latitude%') OR " +
                            "Name LIKE UPPER('%GWTN%'))";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        return;
                    }

                case "seller_button":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                        new List<InlineKeyboardButton[]>()
                        {
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithUrl("Написати", PrivateData.telegramContact)
                            }
                        });
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendContactAsync(
                            chatID,
                            phoneNumber: $"{PrivateData.phoneNumberKyivstar}",
                            firstName: $"{PrivateData.name}",
                            vCard: "BEGIN:VCARD\n" +
                            "VERSION:4.0\n" +
                            $"N:{PrivateData.name}\n" +
                            $"TEL;TYPE=voice,work,pref:{PrivateData.phoneNumberKyivstar}\n" +
                            "END:VCARD",
                            replyMarkup: inlineKeyboard
                        );

                        return;
                    }
                #endregion

                #region Brands
                case "acer_brand_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки Acer!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Brand = 'Acer'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "asus_brand_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки Asus!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Brand = 'Asus'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "dell_brand_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки Dell!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Brand = 'Dell'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "hp_brand_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки HP!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Brand = 'HP'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "lenovo_brand_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки Lenovo!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Brand = 'Lenovo'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "gateway_brand_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки Gateway!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Brand = 'Gateway'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "other_brand_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки Acer!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Brand != 'Acer' OR 'Asus' OR 'Dell' OR 'HP' OR 'Lenovo' OR 'Gateway'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                #endregion

                #region Prices
                case "cheap_price_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки ціною до 5000грн!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Price <= 5000";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "low_price_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки ціною від 5000грн до 8000грн!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Price BETWEEN 5000 AND 8000";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "default_price_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки ціною від 8000грн до 11000грн!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Price BETWEEN 8000 AND 11000";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "mid_price_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки ціною від 11000грн до 15000грн!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Price BETWEEN 11000 AND 15000";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "high_price_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки ціною від 15000грн!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Price >= 15000";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                #endregion

                #region Types
                case "budget_type_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки бюджетного типу!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Type = Budget";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "office_type_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки офісного типу!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Type = Office";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "profi_type_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки професіонального типу!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Type = Professional";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "gaming_type_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки ігрового типу!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Type = Gaming";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                #endregion

                #region Screens
                case "mini_screen_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки з екраном 11-12 дюймів!");

                        command.CommandText = "SELECT * FROM Laptops WHERE (Screen = '11' OR Screen = '12')";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "small_screen_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки з екраном 13-14 дюймів!");

                        command.CommandText = "SELECT * FROM Laptops WHERE (Screen = '13' OR Screen = '14')";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "default_screen_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки з екраном 15-16 дюймів!");

                        command.CommandText = "SELECT * FROM Laptops WHERE (Screen = 15 OR Screen = 16)";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "big_screen_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки з екраном більше 17 дюймів!");

                        command.CommandText = "SELECT * FROM Laptops WHERE Screen >= 17";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                #endregion

                #region CPUs
                case "intel_cpu_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки на процесорі Intel!");

                        command.CommandText = "SELECT * FROM Laptops WHERE CPU = 'Intel'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                case "amd_cpu_Button":
                    {
                        await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                        await telegramBotClient.SendTextMessageAsync(chatID, "Обрано ноутбуки на процесорі AMD!");

                        command.CommandText = "SELECT * FROM Laptops WHERE CPU = 'AMD'";
                        await DatabaseSetup.SelectLaptops(telegramBotClient, command, callbackQuery.Message.Chat);
                        command.Reset();
                        return;
                    }
                    #endregion
            }
        }
    }
}
