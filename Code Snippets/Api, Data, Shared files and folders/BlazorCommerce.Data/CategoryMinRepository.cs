using System;
using System.Collections.Generic;
using BlazorCommerce.Shared;
using Microsoft.Extensions.Options;
using Microsoft.Data.Sqlite;

namespace BlazorCommerce.Data
{
    public class CategoryMinRepository : BaseRepository, IRepository<CategoryMinDto>
    {
        public CategoryMinRepository(IOptions<BlazorCommerce.Data.DatabaseOptions> databaseOptions) : base(databaseOptions) { }

        public IEnumerable<CategoryMinDto> GetAllSiblingCategories(string friendlyUrl)
        {
            var categories = new List<CategoryMinDto>();
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "SELECT c.Id, c.Name, c.FriendlyUrl, c.Hero, c.SingleBanner, c.MidiumBanner FROM LeafCategories c INNER JOIN BranchCategories bc ON c.PrimaryBranchCategoryId = bc.Id INNER JOIN LeafCategories c1 ON bc.Id = c1.PrimaryBranchCategoryId WHERE c1.FriendlyUrl = '" + friendlyUrl + "';";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var category = new CategoryMinDto()    
                            {    
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl")),
                                CategoryType = CalculateCategoryType(rdr)
                            };
                            categories.Add(category);
                        }
                    }
                }

                con.Close();
            }
            return categories;
        }

        public IEnumerable<CategoryMinDto> GetFeaturedCategories()
        {
            var categories = new List<CategoryMinDto>();
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "SELECT c.Id, c.Name, c.FriendlyUrl, c.Hero, c.SingleBanner, c.MidiumBanner FROM LeafCategories c WHERE Hero = 1 OR SingleBanner = 1 OR MidiumBanner = 1;";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var category = new CategoryMinDto()    
                            {    
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl")),
                                CategoryType = CalculateCategoryType(rdr)
                            };
                            categories.Add(category);
                        }
                    }
                }

                con.Close();
            }
            return categories;
        }

        public CategoryMinDto Get(string friendlyUrl)
        {
            CategoryMinDto category = null;
                            
            using (var con = new SqliteConnection(base.ConnectionString))
            {
                con.Open();

                string stm = "SELECT Id, Name, FriendlyUrl, c.Hero, c.SingleBanner, c.MidiumBanner FROM LeafCategories WHERE FriendlyUrl = '" + friendlyUrl + "';";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            category = new CategoryMinDto()    
                            {
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                FriendlyUrl = rdr.GetString(rdr.GetOrdinal("FriendlyUrl")),
                                CategoryType = CalculateCategoryType(rdr)
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
            throw new NotImplementedException();
        }

        public IEnumerable<MenuCategoryDto> GetMenuCategories()
        {
            throw new NotImplementedException();
        }

        private byte CalculateCategoryType(SqliteDataReader rdr)
        {
            if (rdr.GetBoolean(rdr.GetOrdinal("Hero")))
            {
                return 1;
            }
            else if (rdr.GetBoolean(rdr.GetOrdinal("SingleBanner")))
            {
                return 2;
            }
            else if (rdr.GetBoolean(rdr.GetOrdinal("MidiumBanner")))
            {
                return 3;
            }
            return 4;
        }

        public TrendingProductsDto GetTrendingProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryProductDto> GetProductsByLeafCategoryId(int leafCategoryId)
        {
            throw new NotImplementedException();
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