namespace FlipZon.Service
{
    public class DataBase:IDataBase
    {
        SQLiteAsyncConnection sqliteDatabase;

        public DataBase()
        {
            try
            {
                if (sqliteDatabase is not null)
                    return;
                //sqliteDatabase = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                //sqliteDatabase.CreateTableAsync<CartRequestDto>().Wait();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table: {ex.Message}");
            }
        }

        public async Task<int> AddItemToCart(CartRequestDto cart)
        {
            var response = await sqliteDatabase.Table<CartRequestDto>().Where(x => x.ProductId == cart.ProductId).FirstOrDefaultAsync();
            if (response != null)
                return await sqliteDatabase.UpdateAsync(cart);
            else
                return await sqliteDatabase.InsertAsync(cart);
        }

        public async Task<List<CartRequestDto>> GetAllCartItems()
        {
            return await sqliteDatabase.Table<CartRequestDto>().ToListAsync();
        }

        public async Task<int> DeleteCartItem(CartRequestDto cart)
        {
            return await sqliteDatabase.DeleteAsync(cart);
        }

        public async Task<CartRequestDto> GetCartItemById(int id)
        {
            return await sqliteDatabase.Table<CartRequestDto>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

      
        public async Task<int> UpdateCartQuantity(CartRequestDto cart)
        {
            try
            {
                if (cart == null)
                    return 0;

                var response = await GetCartItemById(cart.Id);
                if (response != null)
                {
                    return await sqliteDatabase.UpdateAsync(cart);
                }
            }
            catch (Exception ex)
            {
         
            }
            return 0; 
        }

    }
}

