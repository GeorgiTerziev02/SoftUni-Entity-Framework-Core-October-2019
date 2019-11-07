namespace P03_SalesDatabase.Data
{
    public static class DataValidations
    {
        public static class Product
        {
            public const int ProductNameMaxLength = 50;
            public const int DescriptionMaxLength = 250;
            public const string DescriptionDefaultValue = "No description";
        }

        public static class Store
        {
            public const int StoreNameMaxLength = 80;
        }

        public static class Customer
        {
            public const int CustomerNameMaxLength = 100;
            public const int EmailMaxLength = 80;
        }
    }
}
