using System.Collections.Generic;

namespace BlazorCommerce.Shared
{
    public class TrendingProductsDto
    {
        public List<ProductMinDto> ChildrenAndBabies {get;set;}

        public List<ProductMinDto> Womens {get;set;}

        public List<ProductMinDto> Mens {get;set;}
    }
}