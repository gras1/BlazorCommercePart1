using System.Collections.Generic;
using BlazorCommerce.Shared;
using Microsoft.Extensions.Options;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace BlazorCommerce.Data
{
    public class CategoryRepository : BaseRepository, IRepository<CategoryDto>
    {
        public CategoryRepository(IOptions<BlazorCommerce.Data.DatabaseOptions> databaseOptions) : base(databaseOptions) { }

        public IEnumerable<CategoryDto> GetAllSiblingCategories(string friendlyUrl)
        {
            var categories = new List<CategoryDto>();
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "SELECT c.* FROM LeafCategories c INNER JOIN BranchCategories bc ON c.PrimaryBranchCategoryId = bc.Id INNER JOIN LeafCategories c1 ON bc.Id = c1.PrimaryBranchCategoryId WHERE c1.FriendlyUrl = '" + friendlyUrl + "';";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var category = new CategoryDto()    
                            {    
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                IsHero = rdr.GetBoolean(rdr.GetOrdinal("Hero")),
                                IsSingleBanner = rdr.GetBoolean(rdr.GetOrdinal("SingleBanner")),
                                IsMidiumBanner = rdr.GetBoolean(rdr.GetOrdinal("MidiumBanner")),
                                FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl")),
                                ImageUrl = rdr.IsDBNull(rdr.GetOrdinal("ImageUrl")) ? null : rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                                OfferText = rdr.IsDBNull(rdr.GetOrdinal("OfferText")) ? null : rdr.GetString(rdr.GetOrdinal("OfferText")),
                                MarketingText = rdr.IsDBNull(rdr.GetOrdinal("MarketingText")) ? null : rdr.GetString(rdr.GetOrdinal("MarketingText")),
                                ActionButtonText = rdr.IsDBNull(rdr.GetOrdinal("ActionButtonText")) ? null : rdr.GetString(rdr.GetOrdinal("ActionButtonText")),
                            };
                            categories.Add(category);
                        }
                    }
                }

                con.Close();
            }
            return categories;
        }

        public IEnumerable<CategoryDto> GetFeaturedCategories()
        {
            var categories = new List<CategoryDto>();
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "SELECT c.* FROM LeafCategories c WHERE Hero = 1 OR SingleBanner = 1 OR MidiumBanner = 1;";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var category = new CategoryDto()    
                            {    
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                IsHero = rdr.GetBoolean(rdr.GetOrdinal("Hero")),
                                IsSingleBanner = rdr.GetBoolean(rdr.GetOrdinal("SingleBanner")),
                                IsMidiumBanner = rdr.GetBoolean(rdr.GetOrdinal("MidiumBanner")),
                                FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl")),
                                ImageUrl = rdr.IsDBNull(rdr.GetOrdinal("ImageUrl")) ? null : rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                                OfferText = rdr.IsDBNull(rdr.GetOrdinal("OfferText")) ? null : rdr.GetString(rdr.GetOrdinal("OfferText")),
                                MarketingText = rdr.IsDBNull(rdr.GetOrdinal("MarketingText")) ? null : rdr.GetString(rdr.GetOrdinal("MarketingText")),
                                ActionButtonText = rdr.IsDBNull(rdr.GetOrdinal("ActionButtonText")) ? null : rdr.GetString(rdr.GetOrdinal("ActionButtonText")),
                            };
                            categories.Add(category);
                        }
                    }
                }

                con.Close();
            }
            return categories;
        }

        public CategoryDto Get(string friendlyUrl)
        {
            CategoryDto category = null;
                            
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "SELECT lc.*, mc.Name AS MetaCategoryName, mc.FriendlyName FROM LeafCategories lc INNER JOIN BranchCategories bc ON lc.PrimaryBranchCategoryId = bc.Id INNER JOIN MetaCategories mc ON bc.MetaCategoryId = mc.Id WHERE lc.FriendlyUrl = '" + friendlyUrl + "';";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            category = new CategoryDto()    
                            {
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                IsHero = rdr.GetBoolean(rdr.GetOrdinal("Hero")),
                                IsSingleBanner = rdr.GetBoolean(rdr.GetOrdinal("SingleBanner")),
                                IsMidiumBanner = rdr.GetBoolean(rdr.GetOrdinal("MidiumBanner")),
                                FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl")),
                                ImageUrl = rdr.IsDBNull(rdr.GetOrdinal("ImageUrl")) ? null : rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                                OfferText = rdr.IsDBNull(rdr.GetOrdinal("OfferText")) ? null : rdr.GetString(rdr.GetOrdinal("OfferText")),
                                MarketingText = rdr.IsDBNull(rdr.GetOrdinal("MarketingText")) ? null : rdr.GetString(rdr.GetOrdinal("MarketingText")),
                                ActionButtonText = rdr.IsDBNull(rdr.GetOrdinal("ActionButtonText")) ? null : rdr.GetString(rdr.GetOrdinal("ActionButtonText")),
                                MetaCategory = new MetaCategoryMinDto
                                {
                                    Name = rdr.GetString(rdr.GetOrdinal("MetaCategoryName")),
                                    FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyName"))
                                }
                            };
                        }
                    }
                }

                con.Close();
            }
            return category;
        }

        public BestSellingCategoriesDto GetBestSellingCategoriesProducts()
        {
            var bestSellingCategoriesDto = new BestSellingCategoriesDto();
            var bestSellingCategoriesProductsDtos = new List<BestSellingCategoriesProductsDto>();

            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                //1. Populate Meta Category data regardless
                string stm = "SELECT * FROM MetaCategories ORDER BY Id;";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int id = rdr.GetInt32(rdr.GetOrdinal("Id"));
                            var metaCategory = new BestSellingMetaCategoryDto {
                                Id = id,
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                FriendlyName = rdr.GetString(rdr.GetOrdinal("FriendlyName")),
                                ImageUrl = rdr.GetString(rdr.GetOrdinal("ImageUrl")),
                                BestSellingLeafCategories = new List<CategoryMinDto>()
                            };

                            switch (id)
                            {
                                case 1:
                                    bestSellingCategoriesDto.Womens = metaCategory;
                                    break;
                                case 2:
                                    bestSellingCategoriesDto.Mens = metaCategory;
                                    break;
                                case 3:
                                    bestSellingCategoriesDto.ChildrenAndBabies = metaCategory;
                                    break;
                            }
                        }
                    }
                }

                //2. Retrieve best selling categories products
                stm = @"SELECT lc.Id As LeafCategoryId, lc.Name As LeafCategoryName, lc.FriendlyUrl As LeafCategoryFriendlyUrl, bc.Name As BranchCategoryName, mc.Id AS MetaCategoryId, SUM(od.Quantity) AS TotalQuantity
                        FROM OrderDetails od
                            INNER JOIN ProductOptionProductInstances popi ON od.ProductOptionProductInstanceId = popi.Id
                            INNER JOIN ProductOptionProducts pop ON popi.ProductOptionProductId = pop.Id
                            INNER JOIN Products p ON pop.ProductId = p.Id
                            INNER JOIN LeafCategories lc ON p.LeafCategoryId = lc.Id
                            INNER JOIN BranchCategories bc ON lc.PrimaryBranchCategoryId = bc.Id
                            INNER JOIN MetaCategories mc ON bc.MetaCategoryId = mc.Id
                        GROUP BY lc.Id
                        ORDER BY TotalQuantity DESC
                        LIMIT 12;";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var bestSellingCategoriesProduct = new BestSellingCategoriesProductsDto();
                            
                            bestSellingCategoriesProduct.LeafCategoryId = rdr.GetInt32(rdr.GetOrdinal("LeafCategoryId"));
                            bestSellingCategoriesProduct.LeafCategoryName = rdr.GetString(rdr.GetOrdinal("LeafCategoryName"));
                            bestSellingCategoriesProduct.LeafCategoryFriendlyUrl = rdr.GetString(rdr.GetOrdinal("LeafCategoryFriendlyUrl"));
                            bestSellingCategoriesProduct.BranchCategoryName = rdr.GetString(rdr.GetOrdinal("BranchCategoryName"));
                            bestSellingCategoriesProduct.MetaCategoryId = rdr.GetInt32(rdr.GetOrdinal("MetaCategoryId"));
                            bestSellingCategoriesProduct.TotalQuantity = rdr.GetInt32(rdr.GetOrdinal("TotalQuantity"));

                            bestSellingCategoriesProductsDtos.Add(bestSellingCategoriesProduct);
                        }
                    }
                }

                con.Close();

                //3. Populate the Womens best selling categories
                var menscategories = bestSellingCategoriesProductsDtos.Where(bscpd => bscpd.MetaCategoryId == 1);
                if (menscategories.Any())
                {
                    int mensCategoryCount = 1;
                    foreach (var menscategory in menscategories)
                    {
                        if (mensCategoryCount >= 4) //Limit to 4
                        {
                            break;
                        }
                        var bestSellingLeafCategory = new CategoryMinDto
                        {
                            Id = menscategory.LeafCategoryId,
                            Name = menscategory.BranchCategoryName + " " + menscategory.LeafCategoryName,
                            FriendlyUrl = menscategory.LeafCategoryFriendlyUrl
                        };
                        bestSellingCategoriesDto.Womens.BestSellingLeafCategories.Add(bestSellingLeafCategory);
                        mensCategoryCount++;
                    }
                }

                //4. Populate the Mens best selling categories
                var wommenscategories = bestSellingCategoriesProductsDtos.Where(bscpd => bscpd.MetaCategoryId == 2);
                if (wommenscategories.Any())
                {
                    int womensCategoryCount = 1;
                    foreach (var wommenscategory in wommenscategories)
                    {
                        if (womensCategoryCount >= 4) //Limit to 4
                        {
                            break;
                        }
                        var bestSellingLeafCategory = new CategoryMinDto
                        {
                            Id = wommenscategory.LeafCategoryId,
                            Name = wommenscategory.BranchCategoryName + " " + wommenscategory.LeafCategoryName,
                            FriendlyUrl = wommenscategory.LeafCategoryFriendlyUrl
                        };
                        bestSellingCategoriesDto.Mens.BestSellingLeafCategories.Add(bestSellingLeafCategory);
                        womensCategoryCount++;
                    }
                }

                //5. Populate the Children and Baby best selling categories
                var childrenandbabycategories = bestSellingCategoriesProductsDtos.Where(bscpd => bscpd.MetaCategoryId == 3);
                if (childrenandbabycategories.Any())
                {
                    int childrenandbabyCategoryCount = 1;
                    foreach (var childrenandbabycategory in childrenandbabycategories)
                    {
                        if (childrenandbabyCategoryCount >= 4) //Limit to 4
                        {
                            break;
                        }
                        var bestSellingLeafCategory = new CategoryMinDto
                        {
                            Id = childrenandbabycategory.LeafCategoryId,
                            Name = childrenandbabycategory.BranchCategoryName + " " + childrenandbabycategory.LeafCategoryName,
                            FriendlyUrl = childrenandbabycategory.LeafCategoryFriendlyUrl
                        };
                        bestSellingCategoriesDto.ChildrenAndBabies.BestSellingLeafCategories.Add(bestSellingLeafCategory);
                        childrenandbabyCategoryCount++;
                    }
                }
            }

            return bestSellingCategoriesDto;
        }

        public IEnumerable<MenuCategoryDto> GetMenuCategories()
        {
            var menuCategories = new List<MenuCategoryDto>();

            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "SELECT LongName, FriendlyUrl FROM LeafCategories WHERE MenuCategory = 1 LIMIT 8;";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var menuCategory = new MenuCategoryDto();
                            menuCategory.Name = rdr.GetString(rdr.GetOrdinal("LongName"));
                            menuCategory.FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl"));
                            menuCategories.Add(menuCategory);
                        }
                    }
                }

                con.Close();
            }

            return menuCategories;
        }

        public TrendingProductsDto GetTrendingProducts()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CategoryProductDto> GetProductsByLeafCategoryId(int leafCategoryId)
        {
            throw new System.NotImplementedException();
        }

        public void IncrementNumberOfTimesViewed(string friendlyUrl)
        {
            throw new System.NotImplementedException();
        }

        public void AddItemToCart(string cartGuid, int productOptionProductInstanceId, int quantity)
        {
            throw new System.NotImplementedException();
        }
    }
}