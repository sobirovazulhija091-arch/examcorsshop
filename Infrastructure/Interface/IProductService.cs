public interface IProductService
{
    Task<Response<string>> AddAsync(ProductDto dto);
    Task<Response<string>> UpdateAsync(int productid,UpadteProductDto dto);
    Task<Response<string>> DeleteAsync(int productid);
    Task<Response<List<Product>>> GetAsync();
    Task<Response<Product>> GetByIdAsync(int productid);
    Task<Response<string>> DeleteCreatedAtAsync(int productid , DateTime time);


}