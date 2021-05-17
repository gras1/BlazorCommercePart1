namespace BlazorCommerce.Shared
{
    public class ProductOptionDto
    {
        public int Id {get;set;}
        
        public int ProductOptionProductInstanceId {get;set;}
        
        public string Option {get;set;}
        
        public int Stock {get;set;}
        
        public decimal Price {get;set;}
    }
}