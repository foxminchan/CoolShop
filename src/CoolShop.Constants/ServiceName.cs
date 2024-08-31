namespace CoolShop.Constants;

public static class ServiceName
{
    public static class Dapr
    {
        public const string StateStore = "statestore";
        public const string PubSub = "pubsub";
    }

    public static class Database
    {
        public const string Catalog = "catalogdb";
        public const string Inventory = "inventorydb";
        public const string Ordering = "orderingdb";
        public const string Promotion = "promotiondb";
        public const string Rating = "ratingdb";
    }

    public static class AppId
    {
        public const string Catalog = "catalog-api";
        public const string Inventory = "inventory-api";
        public const string Ordering = "ordering-api";
        public const string Promotion = "promotion-api";
        public const string Rating = "rating-api";
    }
}
