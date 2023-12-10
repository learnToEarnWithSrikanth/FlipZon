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
                sqliteDatabase.CreateTableAsync<AddressModel>().Wait();
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

        public async Task DeleteAllCartItemsForUser(int userId)
        {
            try
            {
                var response = await GetAllCartItems(userId);
                if (response?.Count >= 1)
                {
                    foreach (var item in response)
                    {
                        await sqliteDatabase.DeleteAsync(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
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

        #region Address operations
        public async Task<List<AddressModel>> GetAllAddressByUserId(int userId)
        {
            return await sqliteDatabase.Table<AddressModel>().Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<int> AddAddress(AddressModel addressModel)
        {
            return await sqliteDatabase.InsertAsync(addressModel);
        }
        public async Task<int> DeleteAddress(AddressModel addressModel)
        {
            return await sqliteDatabase.DeleteAsync(addressModel);
        }
        public async Task<int> UpdateAddress(AddressModel addressModel)
        {
            var response = await sqliteDatabase.Table<AddressModel>().Where(x => x.Id == addressModel.Id).FirstOrDefaultAsync();
            if(response!=null)
                return await sqliteDatabase.UpdateAsync(addressModel);
            return 0;
        }
        public async Task<int> UpdateAllAddress()
        {
            var resposne= await GetAllAddressByUserId(Preferences.Get(Constants.USER_ID, -1));
            if (resposne != null & resposne.Count>0)
            {
                resposne.ForEach(x => x.IsSelected = false);
                return await sqliteDatabase.UpdateAllAsync(resposne);
            }
            return 0;
        }
        #endregion
    }
}

