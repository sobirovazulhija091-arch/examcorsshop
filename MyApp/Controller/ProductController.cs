using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController(IProductService product):ControllerBase
{
    private readonly IProductService service = product;
        [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<Response<string>> AddAsync(ProductDto dto)
    {
        return await service.AddAsync(dto);
    }
        [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(int productid,UpadteProductDto dto)
    {
           return await service.UpdateAsync(productid,dto);
    }
        [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int productid)
    {
           return await service.DeleteAsync(productid);
    }
    [HttpGet]
    public async Task<Response<List<Product>>> GetAsync()
    {
        return await service.GetAsync();
    }
    [HttpGet("productid")]
    public async Task<Response<Product>> GetByIdAsync(int productid)
    {
        return await service.GetByIdAsync(productid);
    }
        [Authorize(Roles = "Admin,Manager")]
    [HttpDelete]
    public async Task<Response<string>> DeleteCreatedAtAsync(int productid , DateTime time)
    {
        return await service.DeleteCreatedAtAsync(productid,time);
    }
}