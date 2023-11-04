namespace FlipZon.Service.Interfaces
{
    public interface IDataBase
    {
         Task<int> AddItemToCart(CartRequestDto cart);
        Task<List<CartRequestDto>> GetAllCartItems();
        Task<int> UpdateCartQuantity(CartRequestDto cart);
        Task<CartRequestDto> GetCartItemById(int id);
        Task<int> DeleteCartItem(CartRequestDto cart);
    }
}

