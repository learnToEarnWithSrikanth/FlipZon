namespace FlipZon.Utilities
{
    public class Constants
    {
        #region UI Constants
        public const string APPLICATION_JSON = "application/json";
      
        #endregion

        #region URL'S
        public const string BASE_URL = "https://dummyjson.com";
        public const string GET_PRODUCTS = BASE_URL + "/products?";
        public const string GET_PRODUCT_DETAILS = BASE_URL + "/product";
        public const string GET_CATEGORIES = BASE_URL + "/products/categories";
        public const string GET_PRODUCTS_BY_CATEGEORIES = BASE_URL + "/products/category";
        public const string SEARCH_PRODUCTS = BASE_URL + "/products/search?q=";

        #endregion

        #region Screens
        public const string PRODUCT_DETAILS_SCREEN = "ProductDetailsScreen";
        public const string CART_SCREEN = "CartScreen";
        public const string PRODUCTS_SCREEN= "ProductsScreen";
        #endregion

        #region Navigation Parameters
        public const string PRODUCT_ID = "ProductId";
        #endregion

        #region Database Configuration
        public const string DATABASE_NAME = "FlipZon_db.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DATABASE_NAME);
            }
        }
        #endregion

    }
}

