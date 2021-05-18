namespace BlazorCommerce.Shared
{
    public class ProductMinDto : IDataModel
    {
        public int Id {get;set;}

        public string Name {get;set;}

        public string FriendlyUrl {get;set;}

        public decimal Price {get;set;}

        public string TrendingItemImageUrl {get;set;}
    }
}
