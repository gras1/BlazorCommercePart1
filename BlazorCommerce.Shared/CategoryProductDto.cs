namespace BlazorCommerce.Shared
{
    public class CategoryProductDto
    {
        public int ProductId {get;set;}

        public string Name {get;set;}
        
        public decimal Price {get;set;}

        public string CategoryImageUrl {get;set;}

        public string CategoryHoverImageUrl {get;set;}

        public string FriendlyUrl {get;set;}
    }
}