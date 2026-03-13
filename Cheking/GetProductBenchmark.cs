using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;

[MemoryDiagnoser]
public class GetProductBenchmark
{
  private ApplicationDbContext _context;
  private ProductService _service;

    [GlobalSetup]
    public void Setup()
    {
        _context = TestAppDbContextFactory.CreateContext(nameof(GetProductBenchmark));
        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = NullLogger<ProductService>.Instance;
        _context.Products.AddRange(
            new Product { Id = 1, Name = "Apple", Price = 100, Description = "Test" },
            new Product { Id = 2, Name = "Banana", Price = 90, Description = "Test" },
            new Product { Id = 3, Name = "Cherry", Price = 80, Description = "Test" }
        );
        _context.SaveChanges();
        _service = new ProductService(_context, cache, logger);
    }

    [Benchmark]
    public async Task GetProducts()
    {
        var result = await _service.GetAsync();
    }
}