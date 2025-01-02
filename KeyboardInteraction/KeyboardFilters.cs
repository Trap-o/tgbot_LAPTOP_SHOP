using Telegram.Bot.Types.ReplyMarkups;

namespace tgbot_LAPTOP_SHOP.KeyboardInteraction
{
    static internal class KeyboardFilters
    {
        public static InlineKeyboardMarkup GetMainFilters()
        {
            var getMainFilters = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("За виробником", "brandButton"),
                        InlineKeyboardButton.WithCallbackData("За ціною", "priceButton")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("За призначенням", "typeButton"),
                        InlineKeyboardButton.WithCallbackData("За діагоналлю екрану", "screenButton")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("За процесором", "cpuButton"),
                        InlineKeyboardButton.WithCallbackData("За відеокартою", "videocardButton")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Популярні моделі", "popularButton")
                    }
                }
            );

            return getMainFilters;
        }

        public static InlineKeyboardMarkup GetBrands()
        {
            var getBrands = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Acer", "acer_brand_Button"),
                        InlineKeyboardButton.WithCallbackData("Asus", "asus_brand_Button")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Dell", "dell_brand_Button"),
                        InlineKeyboardButton.WithCallbackData("HP", "hp_brand_Button")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Lenovo", "lenovo_brand_Button"),
                        InlineKeyboardButton.WithCallbackData("Gateway", "gateway_brand_Button")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Інші", "other_brand_Button")
                    }
                }
            );

            return getBrands;
        }

        public static InlineKeyboardMarkup GetPrices()
        {
            var getPrices = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("До 5000грн", "cheap_price_Button"),
                        InlineKeyboardButton.WithCallbackData("5000-8000грн", "low_price_Button")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("8000-11000грн", "default_price_Button"),
                        InlineKeyboardButton.WithCallbackData("11000-15000грн", "mid_price_Button")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("15000+грн", "high_price_Button"),
                    }
                }
            );

            return getPrices;
        }

        public static InlineKeyboardMarkup GetTypes()
        {
            var getTypes = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Бюджетні", "budget_type_Button"),
                        InlineKeyboardButton.WithCallbackData("Офісні", "office_type_Button")

                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Професіональні", "profi_type_Button"),
                        InlineKeyboardButton.WithCallbackData("Ігрові", "gaming_type_Button")
                    }
                }
            );

            return getTypes;
        }

        public static InlineKeyboardMarkup GetScreens()
        {
            var getScreens = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("11-12\"", "mini_screen_Button"),
                        InlineKeyboardButton.WithCallbackData("13-14\"", "small_screen_Button")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("15-16\"", "default_screen_Button"),
                        InlineKeyboardButton.WithCallbackData("17+\"", "big_screen_Button")
                    }
                }
            );

            return getScreens;
        }

        public static InlineKeyboardMarkup GetCPUs()
        {
            var getCPUs = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Intel", "intel_cpu_Button")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("AMD", "amd_cpu_Button")
                    }
                }
            );

            return getCPUs;
        }
    }
}
