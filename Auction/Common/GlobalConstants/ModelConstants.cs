namespace GlobalConstants
{
    public static class ModelConstants
    {
        public static class User
        {
            public const int FullNameMaxLength = 50;
        }

        public static class Item
        {
            public const int NameMaxLength = 120;
            public const int DescriptionMaxLength = 500;
            public const string MinPrice = "0.01";
            public const string MaxPrice = "79228162514264337593543950335";
            public const int MinDuration = 1;
            public const int MaxDuration = 48;
        }

        public static class AuctionHouse
        {
            public const int AuctionNameMaxLength = 30;
            public const int AddressMaxLength = 100;
            public const int DescriptionMaxLength = 500;
        }

        public static class Bid
        {
            public const string MinPrice = "0.01";
            public const string MaxPrice = "79228162514264337593543950335";
        }

        public static class City
        {
            public const int CityNameMaxLength = 30;
        }

        public static class Review
        {
            public const int AuthorNameMaxLength = 50;
            public const int DescriptionMaxLength = 500;
        }
    }
}