using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
public class ProductService(ApplicationDbContext dbContext,IMemoryCache cache,ILogger<ProductService> logger) : IProductService
{
    private readonly  ApplicationDbContext context = dbContext;
    private readonly ILogger<ProductService> _Logger=logger;
    private readonly IMemoryCache memory=cache;
    private const string CacheKey = "product";
    public async Task<Response<string>> AddAsync(ProductDto dto)
    {
       var product = new Product
       {
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description
       };
       await context.AddAsync(product);
       await context.SaveChangesAsync();
       return new Response<string>(HttpStatusCode.OK,"Prodact added");
    }

    public async Task<Response<string>> DeleteAsync(int productid)
    {
       var find = await context.Products.FindAsync(productid);
       context.Products.Remove(find);
       await context.SaveChangesAsync();
       return new Response<string>(HttpStatusCode.OK,"Product Deleted");
    }

    public async Task<Response<List<Product>>> GetAsync()
    {
        var product = await memory.GetOrCreateAsync(CacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            entry.SlidingExpiration = TimeSpan.FromSeconds(2);
            entry.Priority =CacheItemPriority.High;
            
            _Logger.LogInformation("Cache miss for product. Fetching from database...");
            await Task.Delay(1000); 
           return await context.Products.ToListAsync();
          });
    return new Response<List<Product>>(HttpStatusCode.OK, "ok", product);
    }
    
    public  async Task<Response<Product>> GetByIdAsync(int productid)
    {
              var get = await context.Products.FindAsync(productid);
              return new Response<Product>(HttpStatusCode.OK,"Ok",get);
    }

    public async Task<Response<string>> UpdateAsync(int productid, UpadteProductDto dto)
    {
       var up = await context.Products.FindAsync(productid);
       up.Price=dto.Price;
       up.Description=dto.Description;
       up.Name=dto.Name;
       await context.SaveChangesAsync();
       return new Response<string>(HttpStatusCode.OK,"Product Updateed");
    }
   public async Task<Response<string>> DeleteCreatedAtAsync(int productid , DateTime time)
    {
          var product = await context.Products.FindAsync(productid);
         if ((DateTime.Now - product.CreatedAt).TotalMinutes < 2)
      {
        return new Response<string>(HttpStatusCode.BadRequest, "You can delete only after 2 minutes");
      }
    context.Products.Remove(product);
    await context.SaveChangesAsync();
    return new Response<string>(HttpStatusCode.OK, "Product deleted");
    }
 
}