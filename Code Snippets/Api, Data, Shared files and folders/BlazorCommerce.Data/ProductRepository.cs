using System;
using System.Collections.Generic;
using System.Linq;
using BlazorCommerce.Shared;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace BlazorCommerce.Data
{
    public class ProductRepository : BaseRepository, IRepository<ProductDto>
    {
        public ProductRepository(IOptions<BlazorCommerce.Data.DatabaseOptions> databaseOptions) : base(databaseOptions) { }

        public void AddItemToCart(string cartGuid, int productOptionProductInstanceId, int quantity)
        {
            throw new NotImplementedException();
        }

        public ProductDto Get(string friendlyUrl)
        {
            var product = new ProductDto();
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = @"SELECT p.Id, p.Title, p.Sku, p.MainImageUrl, p.MainImageThumbnailUrl, p.SecondImageUrl, p.SecondImageThumbnailUrl, p.ThirdImageUrl,
                               p.ThirdImageThumbnailUrl, p.FourthImageUrl, p.FourthImageThumbnailUrl, p.BriefDescription, p.FullDescription,
                               sm.Name AS SupplierManufacturer, lc.Name AS LeafCategory, lc.FriendlyUrl AS LeafCategoryFriendlyUrl,
                               mc.Name AS MetaCategory, mc.FriendlyName AS MetaCategoryFriendlyUrl, pot.Name AS ProductOptionType
                               FROM Products p
                                   INNER JOIN SupplierManufacturers sm ON p.SupplierManufacturerId = sm.Id
                                   INNER JOIN LeafCategories lc ON p.LeafCategoryId = lc.Id
                                   INNER JOIN BranchCategories bc ON lc.PrimaryBranchCategoryId = bc.Id
                                   INNER JOIN MetaCategories mc ON bc.MetaCategoryId = mc.Id
                                   INNER JOIN ProductOptionTypes pot on p.ProductOptionTypeId = pot.Id
                               WHERE p.FriendlyUrl = '" + friendlyUrl + "';";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            product.Id = rdr.GetInt32(rdr.GetOrdinal("Id"));
                            product.Title = rdr.GetString(rdr.GetOrdinal("Title"));
                            product.Sku = rdr.GetString(rdr.GetOrdinal("Sku"));
                            product.MainImageUrl = rdr.GetString(rdr.GetOrdinal("MainImageUrl"));
                            product.MainImageThumbnailUrl = rdr.GetString(rdr.GetOrdinal("MainImageThumbnailUrl"));
                            product.SecondImageUrl = rdr.GetString(rdr.GetOrdinal("SecondImageUrl"));
                            product.SecondImageThumbnailUrl = rdr.GetString(rdr.GetOrdinal("SecondImageThumbnailUrl"));
                            product.ThirdImageUrl = rdr.GetString(rdr.GetOrdinal("ThirdImageUrl"));
                            product.ThirdImageThumbnailUrl = rdr.GetString(rdr.GetOrdinal("ThirdImageThumbnailUrl"));
                            product.FourthImageUrl = rdr.GetString(rdr.GetOrdinal("FourthImageUrl"));
                            product.FourthImageThumbnailUrl = rdr.GetString(rdr.GetOrdinal("FourthImageThumbnailUrl"));
                            product.BriefDescription = rdr.GetString(rdr.GetOrdinal("BriefDescription"));
                            product.FullDescription = rdr.GetString(rdr.GetOrdinal("FullDescription"));
                            product.SupplierManufacturer = rdr.GetString(rdr.GetOrdinal("SupplierManufacturer"));
                            product.LeafCategory = rdr.GetString(rdr.GetOrdinal("LeafCategory"));
                            product.LeafCategoryFriendlyUrl = rdr.GetString(rdr.GetOrdinal("LeafCategoryFriendlyUrl"));
                            product.MetaCategory = rdr.GetString(rdr.GetOrdinal("MetaCategory"));
                            product.MetaCategoryFriendlyUrl = rdr.GetString(rdr.GetOrdinal("MetaCategoryFriendlyUrl"));
                            product.ProductOptionType = rdr.GetString(rdr.GetOrdinal("ProductOptionType"));
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
                
                var productOptions = new List<ProductOptionDto>();
                stm = @"SELECT po.Id, popi.Id AS ProductOptionProductInstanceId, po.Option, po.SalesTaxTypeId, pop.Stock, popi.Price
                        FROM Products p
                            INNER JOIN ProductOptionProducts pop ON p.Id = pop.ProductId
                            INNER JOIN ProductOptionProductInstances popi ON pop.Id = popi.ProductOptionProductId
                            INNER JOIN ProductOptions po ON pop.ProductOptionId = po.Id
                        WHERE p.Id = " + product.Id + @"
                            AND EffectiveFromDate <= strftime ('%s', 'now')
                            AND (EffectiveToDate IS NULL OR EffectiveToDate > strftime ('%s', 'now'))";
                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var amount = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                            var salesTaxTypeId = rdr.GetInt32(rdr.GetOrdinal("SalesTaxTypeId"));
                            var salesTaxAmount = salesTaxes.First(st => st.SalesTaxTypeId == salesTaxTypeId).Amount;
                            var productOption = new ProductOptionDto
                            {    
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                ProductOptionProductInstanceId = rdr.GetInt32(rdr.GetOrdinal("ProductOptionProductInstanceId")),
                                Option = rdr.GetString(rdr.GetOrdinal("Option")),
                                Stock = rdr.GetInt32(rdr.GetOrdinal("Stock")),
                                Price = Math.Round(amount + (amount * 0.01m * salesTaxAmount), 2, MidpointRounding.AwayFromZero)
                            };
                            productOptions.Add(productOption);
                        }
                    }
                }
                product.ProductOptions = productOptions;
                var mostExpensiveProductOption = productOptions.OrderByDescending(po => po.Price).First();
                product.ProductOptionId = mostExpensiveProductOption.Id;
                product.ProductOptionProductInstanceId = mostExpensiveProductOption.ProductOptionProductInstanceId;
                product.Stock = mostExpensiveProductOption.Stock;
                product.Price = mostExpensiveProductOption.Price;
                product.Option = mostExpensiveProductOption.Option;

                var productFeatures = new List<string>();
                stm = "SELECT pf.Feature FROM ProductFeatures pf INNER JOIN Products p ON pf.ProductId = p.Id WHERE p.Id = " + product.Id + ";";
                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            productFeatures.Add(rdr.GetString(rdr.GetOrdinal("Feature")));
                        }
                    }
                }
                product.Features = productFeatures;
                
                con.Close();
            }
            
            return product;
        }

        public IEnumerable<ProductDto> GetAllSiblingCategories(string friendlyUrl)
        {
            throw new System.NotImplementedException();
        }

        public BestSellingCategoriesDto GetBestSellingCategoriesProducts()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProductDto> GetFeaturedCategories()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MenuCategoryDto> GetMenuCategories()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CategoryProductDto> GetProductsByLeafCategoryId(int leafCategoryId)
        {
            throw new System.NotImplementedException();
        }

        public TrendingProductsDto GetTrendingProducts()
        {
            throw new System.NotImplementedException();
        }

        public void IncrementNumberOfTimesViewed(string friendlyUrl)
        {
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "UPDATE Products SET NumberOfTimesViewed = NumberOfTimesViewed + 1 WHERE FriendlyUrl = '" + friendlyUrl + "';";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    cmd.ExecuteNonQuery();
                }
                
                con.Close();
            }
        } 
    }
}