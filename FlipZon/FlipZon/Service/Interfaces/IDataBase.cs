namespace FlipZon.Service.Interfaces
{
    public interface IDataBase
    {
        #region Cart Operations
        Task<int> AddItemToCart(CartRequestDto cart);
        Task<List<CartRequestDto>> GetAllCartItems(int userId);
        Task<int> UpdateCartQuantity(CartRequestDto cart);
        Task<CartRequestDto> GetCartItemById(int id);
        Task<int> DeleteCartItem(CartRequestDto cart);
        Task<CartRequestDto> GetUserCartItem(int productId, int userId);
        Task DeleteAllCartItemsForUser(int userId);
        #endregion

        #region Account Operations
        Task<int> CreateAccount(SignupModel signupModel);
        Task<SignupModel> GetAccount(SignupModel signupModel);
        #endregion

        #region Address operations

        Task<List<AddressModel>> GetAllAddressByUserId(int userId);

        Task<int> AddAddress(AddressModel addressModel);

        Task<int> DeleteAddress(AddressModel addressModel);

        Task<int> UpdateAddress(AddressModel addressModel);

        Task<int> UpdateAllAddress();

        #endregion
    }
}

