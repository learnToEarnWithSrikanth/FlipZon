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
                sqliteDatabase = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                sqliteDatabase.CreateTableAsync<CartRequestDto>().Wait();
                sqliteDatabase.CreateTableAsync<SignupModel>().Wait();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table: {ex.Message}");
            }
        }


        #region Cart operations
        public async Task<int> AddItemToCart(CartRequestDto cart)
        {
            var response = await sqliteDatabase.Table<CartRequestDto>().Where(x => x.ProductId == cart.ProductId && x.UserId==cart.UserId).FirstOrDefaultAsync();
            if (response != null)
                return await sqliteDatabase.UpdateAsync(cart);
            else
                return await sqliteDatabase.InsertAsync(cart);
        }

        public async Task<List<CartRequestDto>> GetAllCartItems(int userId)
        {
            return await sqliteDatabase.Table<CartRequestDto>().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<int> DeleteCartItem(CartRequestDto cart)
        {
            return await sqliteDatabase.DeleteAsync(cart);
        }

        public async Task<CartRequestDto> GetCartItemById(int id)
        {
            return await sqliteDatabase.Table<CartRequestDto>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<CartRequestDto> GetUserCartItem(int productId,int userId)
        {
            return await sqliteDatabase.Table<CartRequestDto>().Where(x => x.ProductId == productId && x.UserId == userId).FirstOrDefaultAsync();
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
        #endregion

        #region Account Operations
        public async Task<int> CreateAccount(SignupModel signupModel)
        {
            var response = await sqliteDatabase.Table<SignupModel>().Where(x => x.Email == signupModel.Email).FirstOrDefaultAsync();
            if (response != null)
                return await sqliteDatabase.UpdateAsync(signupModel);
            else
                return await sqliteDatabase.InsertAsync(signupModel);
        }

        public async Task<SignupModel> GetAccount(SignupModel signupModel)
        {
            var response = await sqliteDatabase.Table<SignupModel>().Where(x => x.Email == signupModel.Email && x.Password==signupModel.Password).FirstOrDefaultAsync();
            return response;
        }



        #endregion
    }
}

