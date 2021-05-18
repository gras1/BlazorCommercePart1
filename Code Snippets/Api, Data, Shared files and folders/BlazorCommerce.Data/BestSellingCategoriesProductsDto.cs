namespace BlazorCommerce.Data
{
    public class BestSellingCategoriesProductsDto
    {
        public int LeafCategoryId {get;set;}
        
        public string LeafCategoryName {get;set;}
        
        public string LeafCategoryFriendlyUrl {get;set;}
        
        public string BranchCategoryName {get;set;}
        
        public int MetaCategoryId {get;set;}
        
        public int TotalQuantity {get;set;}
    }
}