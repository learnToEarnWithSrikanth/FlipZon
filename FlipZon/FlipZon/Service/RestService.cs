namespace FlipZon.Service
{
    public class RestService:IRestService
    {
        HttpClient httpClient;
        public RestService()
        {
            httpClient = RestServiceHttpClient.Instance.httpClient;
        }

        private void Headers()
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.APPLICATION_JSON));
        }

        public async Task<IResponseResult<ProductsResponeDTO>> GetProducts(int skipCount,int limitCount)
        {
            IResponseResult<ProductsResponeDTO> responseResult = new ResponseResult<ProductsResponeDTO>();
            try
            {
                Headers();
                var itemResource = $"{Constants.GET_PRODUCTS}limit={limitCount}&skip={skipCount}";
                var response = await httpClient.GetAsync(itemResource);
                if (response.IsSuccessStatusCode)
                {
                    responseResult.Result = await JsonConversion.Deserialize<ProductsResponeDTO>(response);
                    responseResult.Status = true;
                }
            }
            catch (Exception ex)
            {

            }
            return responseResult;
        }
        public async Task<IResponseResult<Product>> GetProductDetails(int productId)
        {
            IResponseResult<Product> responseResult = new ResponseResult<Product>();
            try
            {
                Headers();
                var itemResource = $"{Constants.GET_PRODUCT_DETAILS}/{productId}";
                var response = await httpClient.GetAsync(itemResource);
                if (response.IsSuccessStatusCode)
                {
                    responseResult.Result = await JsonConversion.Deserialize<Product>(response);
                    responseResult.Status = true;
                }
            }
            catch (Exception ex)
            {

            }
            return responseResult;
        }
        public async Task<IResponseResult<List<String>>> GetCategories()
        {
            IResponseResult<List<String>> responseResult = new ResponseResult<List<String>>();
            try
            {
                Headers();
                var response = await httpClient.GetAsync(Constants.GET_CATEGORIES);
                if (response.IsSuccessStatusCode)
                {
                    responseResult.Result = await JsonConversion.Deserialize<List<String>>(response);
                    responseResult.Status = true;
                }
            }
            catch (Exception ex)
            {

            }
            return responseResult;
        }
        public async Task<IResponseResult<ProductsResponeDTO>> GetProductsByCategory(String category)
        {
            IResponseResult<ProductsResponeDTO> responseResult = new ResponseResult<ProductsResponeDTO>();
            try
            {
                Headers();
                var itemResource = $"{Constants.GET_PRODUCTS_BY_CATEGEORIES}/{category}";
                var response = await httpClient.GetAsync(itemResource);
                if (response.IsSuccessStatusCode)
                {
                    responseResult.Result = await JsonConversion.Deserialize<ProductsResponeDTO>(response);
                    responseResult.Status = true;
                }
            }
            catch (Exception ex)
            {

            }
            return responseResult;
        }

        public async Task<IResponseResult<ProductsResponeDTO>> SearchProducts(String searchText)
        {
            IResponseResult<ProductsResponeDTO> responseResult = new ResponseResult<ProductsResponeDTO>();
            try
            {
                Headers();
                var itemResource = $"{Constants.SEARCH_PRODUCTS}{searchText}";
                var response = await httpClient.GetAsync(itemResource);
                if (response.IsSuccessStatusCode)
                {
                    responseResult.Result = await JsonConversion.Deserialize<ProductsResponeDTO>(response);
                    responseResult.Status = true;
                }
            }
            catch (Exception ex)
            {

            }
            return responseResult;
        }


    }
}

