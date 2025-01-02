namespace tgbot_LAPTOP_SHOP
{
    public class Goods
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public required string? ImagePath { get; set; }
        public required string? UrlToOLX { get; set; }
    }
}
