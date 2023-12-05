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
        #endregion

        #region Account Operations
        Task<int> CreateAccount(SignupModel signupModel);
        Task<SignupModel> GetAccount(SignupModel signupModel);
        #endregion


    }
}

