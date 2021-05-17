using System.Collections.Generic;
using BlazorCommerce.Shared;

namespace BlazorCommerce.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAllSiblingCategories(string friendlyUrl);
        
        IEnumerable<T> GetFeaturedCategories();

        T Get(string friendlyUrl);

        BestSellingCategoriesDto GetBestSellingCategoriesProducts();

        IEnumerable<MenuCategoryDto> GetMenuCategories();

        TrendingProductsDto GetTrendingProducts();

        IEnumerable<CategoryProductDto> GetProductsByLeafCategoryId(int leafCategoryId);

        void IncrementNumberOfTimesViewed(string friendlyUrl);

        void AddItemToCart(string cartGuid, int productOptionProductInstanceId, int quantity);
    }
}
