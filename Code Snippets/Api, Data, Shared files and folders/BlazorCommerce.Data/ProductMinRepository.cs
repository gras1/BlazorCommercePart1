using System.Collections.Generic;
using BlazorCommerce.Shared;
using Microsoft.Extensions.Options;
using Microsoft.Data.Sqlite;
using System.Linq;
using System;

namespace BlazorCommerce.Data
{
    public class ProductMinRepository : BaseRepository, IRepository<ProductMinDto>
    {
        public ProductMinRepository(IOptions<BlazorCommerce.Data.DatabaseOptions> databaseOptions) : base(databaseOptions) { }

        public ProductMinDto Get(string friendlyUrl)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProductMinDto> GetAllSiblingCategories(string friendlyUrl)
        {
            throw new System.NotImplementedException();
        }

        public BestSellingCategoriesDto GetBestSellingCategoriesProducts()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProductMinDto> GetFeaturedCategories()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MenuCategoryDto> GetMenuCategories()
        {
            throw new System.NotImplementedException();
        }

        public TrendingProductsDto GetTrendingProducts()
        {
            var trendingProducts = new TrendingProductsDto();
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                for (var metaCategoryId = 1; metaCategoryId < 4; metaCategoryId++)
                {
                    var products = new List<ProductMinDto>();
                    string stm = @"SELECT p.Id, p.Title AS Name, p.FriendlyUrl, bc.MetaCategoryId, (popi.Price + round(popi.Price * 0.01 * st.Amount, 2)) AS Price, p.TrendingItemImageUrl, p.NumberOfTimesViewed
                                    FROM ProductOptionProductInstances popi
                                        INNER JOIN ProductOptionProducts pop ON popi.ProductOptionProductId = pop.Id
                                        INNER JOIN ProductOptions po ON pop.ProductOptionId = po.Id
                                        INNER JOIN SalesTaxTypes stt ON po.SalesTaxTypeId = stt.Id
                                        INNER JOIN SalesTaxes st ON stt.Id = st.SalesTaxTypeId
                                        INNER JOIN Products p ON pop.ProductId = p.Id
                                        INNER JOIN LeafCategories lc ON p.LeafCategoryId = lc.Id
                                        INNER JOIN BranchCategories bc ON lc.PrimaryBranchCategoryId = bc.Id
                                    GROUP BY p.Id
                                    HAVING MetaCategoryId = " + metaCategoryId + @"
                                    ORDER BY p.NumberOfTimesViewed DESC
                                    LIMIT 4;";

                    using (var cmd = new SqliteCommand(stm, con))
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var product = new ProductMinDto()    
                                {    
                                    Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                    Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                    FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl")),
                                    Price = rdr.GetDecimal(rdr.GetOrdinal("Price")),
                                    TrendingItemImageUrl = rdr.GetString(rdr.GetOrdinal("TrendingItemImageUrl"))
                                };
                                products.Add(product);
                            }
                        }
                    }

                    switch (metaCategoryId)
                    {
                        case 1:
                            trendingProducts.Womens = products;
                            break;
                        case 2:
                            trendingProducts.Mens = products;
                            break;
                        case 3:
                            trendingProducts.ChildrenAndBabies = products;
                            break;
                    }
                }
                con.Close();
            }

            return trendingProducts;
        }

        public IEnumerable<CategoryProductDto> GetProductsByLeafCategoryId(int leafCategoryId)
        {
            var categoryProducts = new List<CategoryProductDto>();
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = $"SELECT Id, Title, CategoryImageUrl, CategoryHoverImageUrl, FriendlyUrl FROM Products WHERE LeafCategoryId = {leafCategoryId};";
                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var categoryProduct = new CategoryProductDto
                            {    
                                ProductId = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Title")),
                                CategoryImageUrl = rdr.GetString(rdr.GetOrdinal("CategoryImageUrl")),
                                CategoryHoverImageUrl = rdr.GetString(rdr.GetOrdinal("CategoryHoverImageUrl")),
                                FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl"))
                            };
                            categoryProducts.Add(categoryProduct);
                        }
                    }
                }

                var salesTaxes = new List<SalesTax>();
                stm = "SELECT SalesTaxTypeId, Amount FROM SalesTaxes WHERE EffectiveFromDate <= strftime ('%s', 'now') AND (EffectiveToDate IS NULL OR EffectiveToDate > strftime ('%s', 'now'));";
                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var salesTax = new SalesTax
                            {    
                                SalesTaxTypeId = rdr.GetInt32(rdr.GetOrdinal("SalesTaxTypeId")),
                                Amount = rdr.GetDecimal(rdr.GetOrdinal("Amount"))
                            };
                            salesTaxes.Add(salesTax);
                        }
                    }
                }

                foreach (var categoryProduct in categoryProducts)
                {
                    stm = @"SELECT po.SalesTaxTypeId, Max(popi.Price) As MaxPrice
                            FROM Products p
                                INNER JOIN ProductOptionProducts pop ON p.Id = pop.ProductId
                                INNER JOIN ProductOptionProductInstances popi ON pop.Id = popi.ProductOptionProductId
                                INNER JOIN ProductOptions po ON pop.ProductOptionId = po.Id
                            WHERE p.Id = " + categoryProduct.ProductId;
                    using (var cmd = new SqliteCommand(stm, con))
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var salesTaxTypeId = rdr.GetInt32(rdr.GetOrdinal("SalesTaxTypeId"));
                                var amount = rdr.GetDecimal(rdr.GetOrdinal("MaxPrice"));
                                var salesTaxAmount = salesTaxes.First(st => st.SalesTaxTypeId == salesTaxTypeId).Amount;
                                categoryProduct.Price = Math.Round(amount + (amount * 0.01m * salesTaxAmount), 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }
                }

                con.Close();
            }

            return categoryProducts;
        }

        public void IncrementNumberOfTimesViewed(string friendlyUrl)
        {
            throw new NotImplementedException();
        }

        public void AddItemToCart(string cartGuid, int productOptionProductInstanceId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}