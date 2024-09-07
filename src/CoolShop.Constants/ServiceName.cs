namespace CoolShop.Constants;

public static class ServiceName
{
    public const string Blob = "blob";

    public const string Keycloak = "keycloak";

    public const string Completion = "completion";

    public static class Dapr
    {
        public const string StateStore = "statestore";
        public const string PubSub = "pubsub";
        public const string LockStore = "lockstore";
        public const string Smtp = "smtp";
    }

    public static class Database
    {
        public const string Vector = "vector";
        public const string Catalog = "catalogdb";
        public const string Inventory = "inventorydb";
        public const string Ordering = "orderingdb";
        public const string Promotion = "promotiondb";
        public const string Rating = "ratingdb";
    }

    public static class AppId
    {
        public const string Gateway = "gateway";
        public const string Catalog = "catalog-api";
        public const string Inventory = "inventory-api";
        public const string Cart = "cart-api";
        public const string Ordering = "ordering-api";
        public const string Promotion = "promotion-api";
        public const string Rating = "rating-api";
        public const string Notification = "notification-api";
    }
}
