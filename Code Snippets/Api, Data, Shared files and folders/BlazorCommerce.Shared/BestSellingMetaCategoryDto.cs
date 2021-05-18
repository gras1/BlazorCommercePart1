using System.Collections.Generic;

namespace BlazorCommerce.Shared
{
    public class BestSellingMetaCategoryDto
    {
        public int Id {get;set;}

        public string Name {get;set;}

        public string FriendlyName {get;set;}

        public string ImageUrl {get;set;}

        public List<CategoryMinDto> BestSellingLeafCategories {get;set;}
    }
}