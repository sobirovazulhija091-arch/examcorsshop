using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

public class ProductServiceTests
{
    [Fact]
    public async Task AddProductTest()
    {
        await using var context = TestAppDbContextFactory.CreateContext(nameof(AddProductTest));

        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = NullLogger<ProductService>.Instance;

        var service = new ProductService(context, cache, logger);

        var product = new ProductDto
        {
            Name = "Apple",
            Price = 50,
            Description = "This is test"
        };

        var response = await service.AddAsync(product);

        Assert.Equal("Product added", response.Message);
    }

    [Fact]
    public async Task GetProductsTest()
    {
        await using var context = TestAppDbContextFactory.CreateContext(nameof(GetProductsTest));

        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = NullLogger<ProductService>.Instance;

        context.Products.AddRange(
            new Product
            {
                Id = 1,
                Name = "Apple",
                Description = "test",
                Price = 100
            },
            new Product
            {
                Id = 2,
                Name = "Banana",
                Description = "test",
                Price = 90
            },
            new Product
            {
                Id = 3,
                Name = "Cherry",
                Description = "test",
                Price = 80
            }
        );

        await context.SaveChangesAsync();

        var service = new ProductService(context, cache, logger);

        var result = await service.GetAsync();

        Assert.Equal(3, result.Data.Count);
    }

    [Fact]
    public async Task GetByIdTest()
    {
        await using var context = TestAppDbContextFactory.CreateContext(nameof(GetByIdTest));

        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = NullLogger<ProductService>.Instance;

        context.Products.Add(new Product
        {
            Id = 1,
            Name = "Apple",
            Description = "test",
            Price = 100
        });

        await context.SaveChangesAsync();

        var service = new ProductService(context, cache, logger);

        var result = await service.GetByIdAsync(1);

        Assert.Equal("Apple", result.Data.Name);
    }

    [Fact]
    public async Task DeleteProductTest()
    {
        await using var context = TestAppDbContextFactory.CreateContext(nameof(DeleteProductTest));

        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = NullLogger<ProductService>.Instance;

        context.Products.Add(new Product
        {
            Id = 1,
            Name = "Apple",
            Description = "test",
            Price = 100
        });

        await context.SaveChangesAsync();

        var service = new ProductService(context, cache, logger);

        var result = await service.DeleteAsync(1);

        Assert.Equal("Product Deleted", result.Message);
    }
}