namespace FlipZon.Service
{
    public interface IRestService
    {
        Task<IResponseResult<ProductsResponeDTO>> GetProducts(int skipCount, int limitCount);
        Task<IResponseResult<Product>> GetProductDetails(int productId);
        Task<IResponseResult<List<String>>> GetCategories();
        Task<IResponseResult<ProductsResponeDTO>> GetProductsByCategory(String category);
        Task<IResponseResult<ProductsResponeDTO>> SearchProducts(String searchText);
    }
}

