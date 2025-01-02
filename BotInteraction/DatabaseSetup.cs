using System.Data.SQLite;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace tgbot_LAPTOP_SHOP
{
    public static class DatabaseSetup
    {
        public static SQLiteConnection SQLite_connection()
        {
            string ConnectionString = PrivateData.path;
            SQLiteConnection connection = new(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with DB: " + e.Message);
            }

            return connection;
        }

        public static async Task SelectLaptops(ITelegramBotClient telegramBotClient, SQLiteCommand command, Chat chat)
        {
            SQLiteDataReader dataReader;
            List<Goods> laptops = [];
            try
            {
                dataReader = (SQLiteDataReader)await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    laptops.Add(
                        new Goods
                        {
                            Name = dataReader["Name"].ToString(),
                            Description = dataReader["Description"].ToString(),
                            Price = dataReader["Price"].ToString(),
                            ImagePath = dataReader["Image"].ToString(),
                            UrlToOLX = dataReader["URL"].ToString()
                        }
                    );
                }
                await dataReader.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading from database: " + ex.Message);
                await telegramBotClient.SendTextMessageAsync(chat.Id, "Сталася помилка при читанні файлів");
                return;
            }

            if (laptops.Count == 0)
                await telegramBotClient.SendTextMessageAsync(chat.Id, "За даним фільтром ноутбуків в наявності немає!");
            else
            {
                foreach (var laptop in laptops)
                {
                    if (laptop.ImagePath == null)
                    {
                        await telegramBotClient.SendTextMessageAsync(chat.Id, "Інших ноутбуків на даний момент немає!");
                        return;
                    }

                    InputFileStream inputFile = new(new FileStream(laptop.ImagePath, FileMode.Open, FileAccess.Read, FileShare.Read));

                    var inlineKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(
                        new List<InlineKeyboardButton[]>()
                        {
                            new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithUrl("Детальніше", $"{laptop.UrlToOLX}"),
                                    InlineKeyboardButton.WithCallbackData("Написати продавцю", "seller_button")
                                }
                            }
                    );

                    await telegramBotClient.SendPhotoAsync(
                        chat.Id,
                        inputFile,
                        caption: $"Ноутбук {laptop.Name}\n: {laptop.Description}\n\nЦіна – {laptop.Price} грн.",
                        replyMarkup: inlineKeyboard
                    );
                }
                laptops.Clear();
            }
        }

        public static async Task SelectOthers(ITelegramBotClient telegramBotClient, SQLiteCommand command, Telegram.Bot.Types.Chat chat)
        {
            SQLiteDataReader dataReader;
            List<Goods> others = [];
            try
            {
                dataReader = (SQLiteDataReader)await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    others.Add(
                        new Goods
                        {
                            Name = dataReader["Name"].ToString(),
                            Description = dataReader["Description"].ToString(),
                            Price = dataReader["Price"].ToString(),
                            ImagePath = dataReader["Image"].ToString(),
                            UrlToOLX = dataReader["URL"].ToString()
                        }
                    );
                }
                await dataReader.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading from database: " + ex.Message);
                return;
            }
            if (others.Count == 0)
                await telegramBotClient.SendTextMessageAsync(chat.Id, "Інших товарів на даний момент немає!");
            else
            {
                foreach (var other in others)
                {
                    if (other.ImagePath == null)
                    {
                        await telegramBotClient.SendTextMessageAsync(chat.Id, "Інших товарів на даний момент немає!");
                        return;
                    }

                    InputFileStream inputFile = new(new FileStream(other.ImagePath, FileMode.Open, FileAccess.Read, FileShare.Read));

                    var inlineKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(
                        new List<InlineKeyboardButton[]>()
                        {
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithUrl("Детальніше", $"{other.UrlToOLX}"),
                                InlineKeyboardButton.WithCallbackData("Написати продавцю", "seller_button")
                            }
                        }
                    );

                    await telegramBotClient.SendPhotoAsync(
                        chat.Id,
                        inputFile,
                        caption: $"{other.Name}: {other.Description}\n Ціна – {other.Price} грн.",
                        replyMarkup: inlineKeyboard
                    );
                }
                others.Clear();
            }
        }
    }
}
