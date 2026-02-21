public class Product
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
   public string Description{get;set;}=null!;
   public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}