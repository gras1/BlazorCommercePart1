using System.Collections.Generic;

namespace BlazorCommerce.Shared
{
    public class ProductDto : IDataModel
    {
        public int Id {get;set;}

        public string Title {get;set;}

        public string Sku {get;set;}

        public int ProductOptionProductInstanceId {get;set;}
        
        public int ProductOptionId {get;set;}
        
        public string Option {get;set;}
        
        public string ProductOptionType {get;set;}

        public int Stock {get;set;}

        public decimal Price {get;set;}

        public string SupplierManufacturer {get;set;}

        public string MainImageUrl {get;set;}

        public string MainImageThumbnailUrl {get;set;}

        public string SecondImageUrl {get;set;}

        public string SecondImageThumbnailUrl {get;set;}

        public string ThirdImageUrl {get;set;}

        public string ThirdImageThumbnailUrl {get;set;}

        public string FourthImageUrl {get;set;}

        public string FourthImageThumbnailUrl {get;set;}

        public string BriefDescription {get;set;}

        public string FullDescription {get;set;}
        
        public string LeafCategory {get;set;}
        
        public string LeafCategoryFriendlyUrl {get;set;}
        
        public string MetaCategory {get;set;}
        
        public string MetaCategoryFriendlyUrl {get;set;}

        public List<ProductOptionDto> ProductOptions {get;set;}

        public List<string> Features {get;set;}
    }
}