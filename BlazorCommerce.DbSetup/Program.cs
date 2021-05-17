using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BlazorCommerce.DbSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            //If DB does not exist, create it
            connectionStringBuilder.DataSource = "../BlazorCommerce.Data/BlazorCommerce.db";

            DropTables(connectionStringBuilder.ConnectionString);

            CreateMetaCategoriesTable(connectionStringBuilder.ConnectionString);
            PopulateMetaCategoriesTable(connectionStringBuilder.ConnectionString);

            CreateBranchCategoriesTable(connectionStringBuilder.ConnectionString);
            PopulateBranchCategoriesTable(connectionStringBuilder.ConnectionString);

            CreateLeafCategoriesTable(connectionStringBuilder.ConnectionString);
            PopulateLeafCategoriesTable(connectionStringBuilder.ConnectionString);

            CreateCustomersTable(connectionStringBuilder.ConnectionString);
            PopulateCustomersTable(connectionStringBuilder.ConnectionString);

            CreateAddressesTable(connectionStringBuilder.ConnectionString);
            PopulateAddressesTable(connectionStringBuilder.ConnectionString);

            CreateProductOptionTypesTable(connectionStringBuilder.ConnectionString);
            PopulateProductOptionTypesTable(connectionStringBuilder.ConnectionString);

            CreateSupplierManufacturersTable(connectionStringBuilder.ConnectionString);
            PopulateSupplierManufacturersTable(connectionStringBuilder.ConnectionString);

            CreateSalesTaxTypesTable(connectionStringBuilder.ConnectionString);
            PopulateSalesTaxTypesTable(connectionStringBuilder.ConnectionString);

            CreateSalesTaxesTable(connectionStringBuilder.ConnectionString);
            PopulateSalesTaxesTable(connectionStringBuilder.ConnectionString);

            CreateProductsTable(connectionStringBuilder.ConnectionString);
            PopulateProductsTable(connectionStringBuilder.ConnectionString);

            CreateProductFeaturesTable(connectionStringBuilder.ConnectionString);
            PopulateProductFeaturesTable(connectionStringBuilder.ConnectionString);

            CreateProductOptionsTable(connectionStringBuilder.ConnectionString);
            PopulateProductOptionsTable(connectionStringBuilder.ConnectionString);

            CreateProductOptionProductsTable(connectionStringBuilder.ConnectionString);
            PopulateProductOptionProductsTable(connectionStringBuilder.ConnectionString);

            CreateProductOptionProductInstancesTable(connectionStringBuilder.ConnectionString);
            PopulateProductOptionProductInstancesTable(connectionStringBuilder.ConnectionString);

            CreateOrdersTable(connectionStringBuilder.ConnectionString);

            CreateOrderDetailsTable(connectionStringBuilder.ConnectionString);
            
            PopulateOrdersAndOrderDetailsTable(connectionStringBuilder.ConnectionString);
            
            CreateCartsTable(connectionStringBuilder.ConnectionString);

            CreateCartItemsTable(connectionStringBuilder.ConnectionString);
        }

        private static void DropTables(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var delTableCmd = connection.CreateCommand();
                
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Categories";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS CategoryTypes";
                delTableCmd.ExecuteNonQuery();

                delTableCmd.CommandText = "DROP TABLE IF EXISTS CartItems";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Carts";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS OrderDetails";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Orders";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS ProductOptionProductInstances";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS ProductOptionProducts";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS ProductOptions";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS SalesTaxes";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS SalesTaxTypes";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS ProductFeatures";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Products";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS LeafCategories";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS BranchCategories";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS MetaCategories";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS SupplierManufacturers";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS ProductOptionTypes";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Addresses";
                delTableCmd.ExecuteNonQuery();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Customers";
                delTableCmd.ExecuteNonQuery();
            }
        }

        private static void CreateMetaCategoriesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE MetaCategories(Id INTEGER PRIMARY KEY ASC, Name VARCHAR(20) NOT NULL, FriendlyName VARCHAR(20) NOT NULL, ImageUrl VARCHAR(100))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateMetaCategoriesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO MetaCategories VALUES(1, 'Women''s', 'womens', 'https://via.placeholder.com/225x155')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO MetaCategories VALUES(2, 'Men''s', 'mens', 'https://via.placeholder.com/225x155')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO MetaCategories VALUES(3, 'Children & Babies', 'children-babies', 'https://via.placeholder.com/225x155')";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateBranchCategoriesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE BranchCategories(Id INTEGER PRIMARY KEY ASC, Name VARCHAR(20) NOT NULL, MetaCategoryId INTEGER NOT NULL, FOREIGN KEY(MetaCategoryId) REFERENCES MetaCategories(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateBranchCategoriesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(1, 'Clothing', 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(2, 'Tops', 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(3, 'Dresses', 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(4, 'Summer', 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(5, 'Shoes & Accessories', 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(6, 'Clothing', 2)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(7, 'Tops', 2)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(8, 'Sports', 2)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(9, 'Shoes & Accessories', 2)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(10, 'Toys & Games', 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(11, 'Girls'' Clothing', 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(12, 'Boys'' Clothing', 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(13, 'Baby Girls'' Clothing', 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(14, 'Baby Boys'' Clothing', 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO BranchCategories VALUES(15, 'Shoes & Accessories', 3)";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateLeafCategoriesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE LeafCategories(Id INTEGER PRIMARY KEY ASC, Name VARCHAR(50) NOT NULL, LongName VARCHAR(100) NOT NULL, FriendlyUrl VARCHAR(100) NOT NULL, ImageUrl VARCHAR(100), OfferText VARCHAR(100), MarketingText VARCHAR(250), ActionButtonText VARCHAR(20), MenuCategory BOOLEAN NOT NULL, Hero BOOLEAN NOT NULL, SingleBanner BOOLEAN NOT NULL, MidiumBanner BOOLEAN NOT NULL, PrimaryBranchCategoryId INTEGER NOT NULL, SecondaryBranchCategoryId INTEGER, FOREIGN KEY(PrimaryBranchCategoryId) REFERENCES BranchCategories(Id), FOREIGN KEY(SecondaryBranchCategoryId) REFERENCES BranchCategories(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateLeafCategoriesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(1, 'Coats & Jackets', 'Women''s Coats & Jackets', 'womens-clothing-coats-and-jackets', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(2, 'Jumpers & Cardigans', 'Women''s Jumpers & Cardigans', 'womens-clothing-jumpers-and-cardigans', 'https://via.placeholder.com/600x370', NULL, 'Lorem ipsum __ dolor', 'Discover Now', 0, 0, 1, 0, 1, 2)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(3, 'Jeans', 'Women''s Jeans', 'womens-clothing-jeans', 'https://via.placeholder.com/600x370', NULL, 'Lorem ipsum __ dolor', 'Shop Now', 0, 0, 1, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(4, 'Skirts', 'Women''s Skirts', 'womens-clothing-skirts', 'https://via.placeholder.com/600x370', NULL, 'Lorem ipsum __ Up to _-40%-_ Off', 'Discover Now', 0, 0, 1, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(5, 'Beachwear', 'Women''s Beachwear', 'womens-summer-beachwear', 'https://via.placeholder.com/600x370', NULL, 'Lorem ipsum __Up to_- 50%-_ off', 'Shop Now', 0, 0, 0, 1, 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(6, 'Shorts', 'Women''s Shorts', 'womens-clothing-shorts', 'https://via.placeholder.com/600x370', NULL, 'Lorem ipsum __ up to _-70%-_ off', 'Shop Now', 0, 0, 0, 1, 1, 4)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(7, 'Hoodies & Sweatshirts', 'Women''s Hoodies & Sweatshirts', 'womens-clothing-hoodies-and-sweatshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, 2)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(8, 'Joggers & Tracksuits', 'Women''s Joggers & Tracksuits', 'womens-clothing-joggers-and-tracksuits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(9, 'Suits', 'Women''s Suits', 'womens-clothing-suits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(10, 'Lingerie', 'Women''s Lingerie', 'womens-clothing-lingerie', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(11, 'Nightwear', 'Women''s Nightwear', 'womens-clothing-nightwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(12, 'Trousers', 'Women''s Trousers', 'womens-clothing-trousers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(13, 'Leggings', 'Women''s Leggings', 'womens-clothing-leggings', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(14, 'T-Shirts', 'Women''s T-Shirts', 'womens-tops-tshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(15, 'Summer Tops', 'Women''s Summer Tops', 'womens-tops-summer-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(16, 'Day Tops', 'Women''s Day Tops', 'womens-tops-day-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(17, 'Blouses & Shirts', 'Women''s Blouses & Shirts', 'womens-tops-blouses-and-shirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(18, 'Bodysuits', 'Women''s Bodysuits', 'womens-tops-bodysuits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(19, 'Crop Tops', 'Women''s Crop Tops', 'womens-tops-crop-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, 4)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(20, 'Halter Neck Tops', 'Women''s Halter Neck Tops', 'womens-tops-halter-neck-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(21, 'Vest Tops', 'Women''s Vest Tops', 'womens-tops-vest-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(22, 'Off The Shoulder Tops', 'Women''s Off The Shoulder Tops', 'womens-tops-off-the-shoulder-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(23, 'Smock Tops', 'Women''s Smock Tops', 'womens-tops-smock-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(24, 'Wrap Tops', 'Women''s Wrap Tops', 'womens-tops-wrap-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(25, 'Plus Size Tops', 'Women''s Plus Size Tops', 'womens-tops-plus-size-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(26, 'Maternity Tops', 'Women''s Maternity Tops', 'womens-tops-maternity-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(27, 'Going Out Tops', 'Women''s Going Out Tops', 'womens-tops-going-out-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 2, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(28, 'Midi Dresses', 'Women''s Midi Dresses', 'womens-dresses-midi-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(29, 'Maxi Dresses', 'Women''s Maxi Dresses', 'womens-dresses-maxi-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, 4)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(30, 'Mini Dresses', 'Women''s Mini Dresses', 'womens-dresses-mini-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(31, 'Smock Dresses', 'Women''s Smock Dresses', 'womens-dresses-smock-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, 4)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(32, 'Shirt Dresses', 'Women''s Shirt Dresses', 'womens-dresses-shirt-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(33, 'Long Sleeve Dresses', 'Women''s Long Sleeve Dresses', 'womens-dresses-long-sleeve-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(34, 'Bodycon Dresses', 'Women''s Bodycon Dresses', 'womens-dresses-bodycon-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(35, 'Skater Dresses', 'Women''s Skater Dresses', 'womens-dresses-skater-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(36, 'Wrap Dresses', 'Women''s Wrap Dresses', 'womens-dresses-wrap-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(37, 'Sweatshirt Dresses', 'Women''s Sweatshirt Dresses', 'womens-dresses-sweatshirt-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(38, 'Black Dresses', 'Women''s Black Dresses', 'womens-dresses-black-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(39, 'Blazer Dresses', 'Women''s Blazer Dresses', 'womens-dresses-blazer-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(40, 'Pinafore Dresses', 'Women''s Pinafore Dresses', 'womens-dresses-pinafore-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(41, 'Jumper Dresses', 'Women''s Jumper Dresses', 'womens-dresses-jumper-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 3, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(42, 'Summer Dresses', 'Women''s Summer Dresses', 'womens-summer-summer-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 4, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(43, 'T-Shirt Dresses', 'Women''s T-Shirt Dresses', 'womens-summer-tshirt-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(44, 'Summer Tops', 'Women''s Summer Tops', 'womens-summer-summer-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(45, 'Kimonos', 'Women''s Kimonos', 'womens-summer-kimonos', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(46, 'Jumpsuits & Playsuits', 'Women''s Jumpsuits & Playsuits', 'womens-summer-jumpsuits-and-playsuits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(47, 'Swimwear', 'Women''s Swimwear', 'womens-summer-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(48, 'Plus Size Swimwear', 'Women''s Plus Size Swimwear', 'womens-summer-plus-size-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(49, 'Maternity Swimwear', 'Women''s Maternity Swimwear', 'womens-summer-maternity-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(50, 'Swimsuits', 'Women''s Swimsuits', 'womens-summer-swimsuits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(51, 'Bikinis', 'Women''s Bikinis', 'womens-summer-bikinis', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 4, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(52, 'Boots', 'Women''s Boots', 'womens-shoes-and-accessories-boots', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(53, 'Sandals', 'Women''s Sandals', 'womens-shoes-and-accessories-sandals', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(54, 'Trainers', 'Women''s Trainers', 'womens-shoes-and-accessories-trainers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(55, 'Flats', 'Women''s Flats', 'womens-shoes-and-accessories-flats', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(56, 'Slippers', 'Women''s Slippers', 'womens-shoes-and-accessories-slippers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(57, 'High Heels', 'Women''s High Heels', 'womens-shoes-and-accessories-high-heels', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(58, 'Wide Fit Shoes', 'Women''s Wide Fit Shoes', 'womens-shoes-and-accessories-wide-fit-shoes', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(59, 'Sunglasses', 'Women''s Sunglasses', 'womens-shoes-and-accessories-sunglasses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(60, 'Bags', 'Women''s Bags', 'womens-shoes-and-accessories-bags', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(61, 'Jewellery', 'Women''s Jewellery', 'womens-shoes-and-accessories-jewellery', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(62, 'Necklaces', 'Women''s Necklaces', 'womens-shoes-and-accessories-necklaces', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(63, 'Belts', 'Women''s Belts', 'womens-shoes-and-accessories-belts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(64, 'Hair Accessories', 'Women''s Hair Accessories', 'womens-shoes-and-accessories-hair-accessories', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(65, 'Tights & Socks', 'Women''s Tights & Socks', 'womens-clothes-tights-and-socks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 1, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(66, 'Scarves, Hats & Gloves', 'Women''s Scarves, Hats & Gloves', 'womens-shoes-and-accessories-scarves-hats-and-gloves', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(67, 'Watches', 'Women''s Watches', 'womens-shoes-and-accessories-watches', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 5, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(68, 'T-Shirts & Vests', 'Men''s T-Shirts & Vests', 'mens-clothing-tshirts-and-vests', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(69, 'Tracksuits', 'Men''s Tracksuits', 'mens-sports-tracksuits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 8, 6)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(70, 'Hoodies & Sweatshirts', 'Men''s Hoodies & Sweatshirts', 'mens-clothing-hoodies-and-sweatshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(71, 'Joggers', 'Men''s Joggers', 'mens-sports-joggers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 8, 6)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(72, 'Jeans', 'Men''s Jeans', 'mens-clothing-jeans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(73, 'Coats & Jackets', 'Men''s Coats & Jackets', 'mens-clothing-coats-and-jackets', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(74, 'Shorts', 'Men''s Shorts', 'mens-clothing-shorts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, 8)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(75, 'Shirts', 'Men''s Shirts', 'mens-tops-shirts', 'https://via.placeholder.com/600x370', '_-UP TO 50% OFF -_Mens Shirts', 'Lorem ipsum dolor sit amet __ consectetur adipiscing elit, sed do eiusmod __ tempor incididunt.', 'Shop Now!', 0, 1, 0, 0, 7, 6)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(76, 'Jumpers & Cardigans', 'Men''s Jumpers & Cardigans', 'mens-clothing-jumpers-and-cardigans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 6, 7)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(77, 'Trousers', 'Men''s Trousers', 'mens-clothing-trousers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(78, 'Denim', 'Men''s Denim', 'mens-clothing-denim', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(79, 'Underwear & Socks', 'Men''s Underwear & Socks', 'mens-clothing-underwear-and-socks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(80, 'Suits', 'Men''s Suits', 'mens-clothing-suits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(81, 'Swimwear', 'Men''s Swimwear', 'mens-sports-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 8, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(82, 'Onesies & Loungewear', 'Men''s Onesies & Loungewear', 'mens-clothing-onesies-and-loungewear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(83, 'Nightwear', 'Men''s Nightwear', 'mens-clothing-nightwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 6, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(84, 'T-Shirts', 'Men''s T-Shirts', 'mens-tops-tshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, 8)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(85, 'Hoodies', 'Men''s Hoodies', 'mens-tops-hoodies', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(86, 'Sweatshirts', 'Men''s Sweatshirts', 'mens-tops-sweatshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(87, 'Polo Shirts', 'Men''s Polo Shirts', 'mens-tops-polo-shirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(88, 'Long Sleeve Shirts', 'Men''s Long Sleeve Shirts', 'mens-tops-long-sleeve-sShirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(89, 'Short Sleeve Shirts', 'Men''s Short Sleeve Shirts', 'mens-tops-short-sleeve-shirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(90, 'Checked Shirts', 'Men''s Checked Shirts', 'mens-tops-checked-shirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(91, 'Printed Shirts', 'Men''s Printed Shirts', 'mens-tops-printed-shirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(92, 'Long Sleeve T-Shirts', 'Men''s Long Sleeve T-Shirts', 'mens-tops-long-sleeve-tshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(93, 'Striped T-Shirts', 'Men''s Striped T-Shirts', 'mens-tops-striped-tshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(94, 'Printed T-Shirts', 'Men''s Printed T-Shirts', 'mens-tops-printed-tshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(95, 'Tank Tops', 'Men''s Tank Tops', 'mens-tops-tank-tops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 7, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(96, 'Trainers & Hi-Tops', 'Men''s Trainers & Hi-Tops', 'mens-shoes-and-accessories-trainers-and-hitops', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(97, 'Sandals & Sliders', 'Men''s Sandals & Sliders', 'mens-shoes-and-accessories-sandals-and-sliders', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(98, 'Shoes & Loafers', 'Men''s Shoes & Loafers', 'mens-shoes-and-accessories-shoes-and-loafers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(99, 'Boots', 'Men''s Boots', 'mens-shoes-and-accessories-boots', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(100, 'Slippers', 'Men''s Slippers', 'mens-shoes-and-accessories-slippers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(101, 'Sunglasses', 'Men''s Sunglasses', 'mens-shoes-and-accessories-sunglasses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(102, 'Hats & Caps', 'Men''s Hats & Caps', 'mens-shoes-and-accessories-hats-and-caps', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(103, 'Jewellery & Watches', 'Men''s Jewellery & Watches', 'mens-shoes-and-accessories-jewellery-and-watches', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(104, 'Bags & Wallets', 'Men''s Bags & Wallets', 'mens-shoes-and-accessories-bags-and-wallets', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(105, 'Belts', 'Men''s Belts', 'mens-shoes-and-accessories-belts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 9, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(106, 'Arts & Crafts', 'Children & Baby Arts & Crafts', 'children-and-baby-toys-and-games-arts-and-crafts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(107, 'Baby & Toddler Toys', 'Children & Baby Baby & Toddler Toys', 'children-and-baby-toys-and-games-baby-and-toddler-toys', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(108, 'Building & Construction Toys', 'Children & Baby Building & Construction Toys', 'children-and-baby-toys-and-games-building-and-construction-toys', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(109, 'Dolls & Accessories', 'Children & Baby Dolls & Accessories', 'children-and-baby-toys-and-games-dolls-and-accessories', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(110, 'Electronic Toys', 'Children''s Electronic Toys', 'children-and-baby-toys-and-games-electronic-toys', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(111, 'Fancy Dress', 'Children & Baby Fancy Dress', 'children-and-baby-toys-and-games-fancy-dress', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(112, 'Jigsaws & Puzzles', 'Children & Baby Jigsaws & Puzzles', 'children-and-baby-toys-and-games-jigsaws-and-puzzles', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(113, 'Kids'' Furniture, Décor & Storage', 'Children & Baby Kids'' Furniture, Décor & Storage', 'children-and-baby-toys-and-games-kids-furniture-decor-and-storage', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(114, 'Learning & Education', 'Children & Baby Learning & Education', 'children-and-baby-toys-and-games-learning-and-education', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(115, 'Musical Toy Instruments', 'Children & Baby Musical Toy Instruments', 'children-and-baby-toys-and-games-musical-toy-instruments', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(116, 'Play Figures & Vehicles', 'Children & Baby Play Figures & Vehicles', 'children-and-baby-toys-and-games-play-figures-and-vehicles', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(117, 'Puppets & Puppet Theatres', 'Children & Baby Puppets & Puppet Theatres', 'children-and-baby-toys-and-games-puppets-and-puppet-theatres', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(118, 'Soft Toys', 'Children & Baby Soft Toys', 'children-and-baby-toys-and-games-soft-toys', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 10, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(119, 'Underwear & Socks', 'Children & Baby Underwear & Socks', 'children-and-baby-girls-clothing-underwear-and-socks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(120, 'T-Shirts', 'Children & Baby T-Shirts', 'children-and-baby-girls-clothing-tshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(121, 'School Uniform', 'Children & Baby School Uniform', 'children-and-baby-girls-clothing-school-uniform', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(122, 'Trousers', 'Children & Baby Trousers', 'children-and-baby-girls-clothing-trousers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(123, 'Blouses & Shirts', 'Children & Baby Blouses & Shirts', 'children-and-baby-girls-clothing-blouses-and-shirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(124, 'Dresses', 'Children & Baby Dresses', 'children-and-baby-girls-clothing-dresses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(125, 'Skirts', 'Children & Baby Skirts', 'children-and-baby-girls-clothing-skirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(126, 'Coats & Jackets', 'Children & Baby Coats & Jackets', 'children-and-baby-girls-clothing-coats-and-jackets', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(127, 'Jumpers & Cardigans', 'Children & Baby Jumpers & Cardigans', 'children-and-baby-girls-clothing-jumpers-and-cardigans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(128, 'Jeans', 'Children & Baby Jeans', 'children-and-baby-girls-clothing-jeans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 1, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(129, 'Swimwear', 'Children & Baby Swimwear', 'children-and-baby-girls-clothing-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(130, 'Shorts', 'Children & Baby Shorts', 'children-and-baby-girls-clothing-shorts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(131, 'Hoodies & Sweatshirts', 'Children & Baby Hoodies & Sweatshirts', 'children-and-baby-girls-clothing-hoodies-and-sweatshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(132, 'Joggers & Tracksuits', 'Children & Baby Joggers & Tracksuits', 'children-and-baby-girls-clothing-joggers-and-tracksuits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(133, 'Nightwear', 'Children & Baby Nightwear', 'children-and-baby-girls-clothing-nightwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(134, 'Leggings', 'Children & Baby Leggings', 'children-and-baby-girls-clothing-leggings', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 11, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(135, 'Underwear & Socks', 'Children & Baby Underwear & Socks', 'children-and-baby-boys-clothing-underwear-and-socks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(136, 'T-Shirts', 'Children & Baby T-Shirts', 'children-and-baby-boys-clothing-tshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(137, 'School Uniform', 'Children & Baby School Uniform', 'children-and-baby-boys-clothing-school-uniform', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(138, 'Trousers', 'Children & Baby Trousers', 'children-and-baby-boys-clothing-trousers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(139, 'Shirts', 'Children & Baby Shirts', 'children-and-baby-boys-clothing-shirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(140, 'Shorts', 'Children & Baby Shorts', 'children-and-baby-boys-clothing-shorts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(141, 'T-Shirts & Vests', 'Children & Baby T-Shirts & Vests', 'children-and-baby-boys-clothing-tshirts-and-vests', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(142, 'Tracksuits', 'Children & Baby Tracksuits', 'children-and-baby-boys-clothing-tracksuits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(143, 'Hoodies & Sweatshirts', 'Children & Baby Hoodies & Sweatshirts', 'children-and-baby-boys-clothing-hoodies-and-sweatshirts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(144, 'Jeans', 'Children & Baby Jeans', 'children-and-baby-boys-clothing-jeans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(145, 'Coats & Jackets', 'Children & Baby Coats & Jackets', 'children-and-baby-boys-clothing-coats-and-jackets', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(146, 'Jumpers & Cardigans', 'Children & Baby Jumpers & Cardigans', 'children-and-baby-boys-clothing-jumpers-and-cardigans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(147, 'Suits', 'Children & Baby Suits', 'children-and-baby-boys-clothing-suits', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(148, 'Swimwear', 'Children & Baby Swimwear', 'children-and-baby-boys-clothing-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(149, 'Onesies', 'Children & Baby Onesies', 'children-and-baby-boys-clothing-onesies', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(150, 'Nightwear', 'Children & Baby Nightwear', 'children-and-baby-boys-clothing-nightwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 12, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(151, 'Swaddle', 'Children & Baby Swaddle', 'children-and-baby-baby-girls-clothing-swaddle', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(152, 'Babygrow & Onesies', 'Children & Baby Babygrow & Onesies', 'children-and-baby-baby-girls-clothing-babygrow-and-onesies', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(153, 'Jumpers & Cardigans', 'Children & Baby Jumpers & Cardigans', 'children-and-baby-baby-girls-clothing-jumpers-and-cardigans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(154, 'Leggings', 'Children & Baby Leggings', 'children-and-baby-baby-girls-clothing-leggings', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(155, 'T-Shirt & Vests', 'Children & Baby T-Shirt & Vests', 'children-and-baby-baby-girls-clothing-tshirt-and-vests', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(156, 'Coats & Jackets', 'Children & Baby Coats & Jackets', 'children-and-baby-baby-girls-clothing-coats-and-jackets', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(157, 'Swimwear', 'Children & Baby Swimwear', 'children-and-baby-baby-girls-clothing-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(158, 'Nightwear', 'Children & Baby Nightwear', 'children-and-baby-baby-girls-clothing-nightwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(159, 'Socks', 'Children & Baby Socks', 'children-and-baby-baby-girls-clothing-socks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(160, 'Shorts', 'Children & Baby Shorts', 'children-and-baby-baby-girls-clothing-shorts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(161, 'Trousers', 'Children & Baby Trousers', 'children-and-baby-baby-girls-clothing-trousers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 13, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(162, 'Swaddle', 'Children & Baby Swaddle', 'children-and-baby-baby-boys-clothing-swaddle', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(163, 'Babygrow & Onesies', 'Children & Baby Babygrow & Onesies', 'children-and-baby-baby-boys-clothing-babygrow-and-onesies', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(164, 'Jumpers & Cardigans', 'Children & Baby Jumpers & Cardigans', 'children-and-baby-baby-boys-clothing-jumpers-and-cardigans', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(165, 'T-Shirt & Vests', 'Children & Baby T-Shirt & Vests', 'children-and-baby-baby-boys-clothing-tshirt-and-vests', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(166, 'Coats & Jackets', 'Children & Baby Coats & Jackets', 'children-and-baby-baby-boys-clothing-coats-and-jackets', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(167, 'Swimwear', 'Children & Baby Swimwear', 'children-and-baby-baby-boys-clothing-swimwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(168, 'Nightwear', 'Children & Baby Nightwear', 'children-and-baby-baby-boys-clothing-nightwear', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(169, 'Socks', 'Children & Baby Socks', 'children-and-baby-baby-boys-clothing-socks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(170, 'Shorts', 'Children & Baby Shorts', 'children-and-baby-baby-boys-clothing-shorts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(171, 'Trousers', 'Children & Baby Trousers', 'children-and-baby-baby-boys-clothing-trousers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 14, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(172, 'Girls Boots', 'Children & Baby Girls Boots', 'children-and-baby-shoes-and-accessories-girls-boots', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(173, 'Girls Sandals', 'Children & Baby Girls Sandals', 'children-and-baby-shoes-and-accessories-girls-sandals', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(174, 'Girls Trainers', 'Children & Baby Girls Trainers', 'children-and-baby-shoes-and-accessories-girls-trainers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(175, 'Girls Slippers', 'Children & Baby Girls Slippers', 'children-and-baby-shoes-and-accessories-girls-slippers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(176, 'Girls Flat Shoes', 'Children & Baby Girls Flat Shoes', 'children-and-baby-shoes-and-accessories-girls-flat-shoes', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(177, 'Girls Bags & Backpacks', 'Children & Baby Girls Bags & Backpacks', 'children-and-baby-shoes-and-accessories-girls-bags-and-backpacks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(178, 'Girls Sunglasses', 'Children & Baby Girls Sunglasses', 'children-and-baby-shoes-and-accessories-girls-sunglasses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(179, 'Girls Hats & Caps', 'Children & Baby Girls Hats & Caps', 'children-and-baby-shoes-and-accessories-girls-hats-and-caps', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(180, 'Girls Belts', 'Children & Baby Girls Belts', 'children-and-baby-shoes-and-accessories-girls-belts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(181, 'Boys Boots', 'Children & Baby Boys Boots', 'children-and-baby-shoes-and-accessories-boys-boots', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(182, 'Boys Sandals', 'Children & Baby Boys Sandals', 'children-and-baby-shoes-and-accessories-boys-sandals', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(183, 'Boys Trainers', 'Children & Baby Boys Trainers', 'children-and-baby-shoes-and-accessories-boys-trainers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(184, 'Boys Slippers', 'Children & Baby Boys Slippers', 'children-and-baby-shoes-and-accessories-boys-slippers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(185, 'Boys Shoes & Loafers', 'Children & Baby Boys Shoes & Loafers', 'children-and-baby-shoes-and-accessories-boys-shoes-and-loafers', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(186, 'Boys Bags & Backpacks', 'Children & Baby Boys Bags & Backpacks', 'children-and-baby-shoes-and-accessories-boys-bags-and-backpacks', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(187, 'Boys Sunglasses', 'Children & Baby Boys Sunglasses', 'children-and-baby-shoes-and-accessories-boys-sunglasses', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(188, 'Boys Hats & Caps', 'Children & Baby Boys Hats & Caps', 'children-and-baby-shoes-and-accessories-boys-hats-and-caps', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO LeafCategories VALUES(189, 'Boys Belts', 'Children & Baby Boys Belts', 'children-and-baby-shoes-and-accessories-boys-belts', 'https://via.placeholder.com/600x370', NULL, NULL, 'Shop Now', 0, 0, 0, 0, 15, NULL)";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
        }

        private static void CreateCustomersTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Customers(Id INTEGER PRIMARY KEY AUTOINCREMENT, EmailAddress VARCHAR(320) NOT NULL, FirstName VARCHAR(100) NOT NULL, Surname VARCHAR(100) NOT NULL, IsRegistered BOOLEAN NOT NULL, Password VARCHAR(100) NOT NULL, CreatedDateTime INTEGER NOT NULL, LastAccessedDateTime INTEGER)";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateCustomersTable(string connectionString)
        {
            //this is temporary test data
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('mike1@test.com', 'Mike', 'One', 0, '', strftime ('%s', '2021-05-12 08:12:01'), NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('sheikh2@test.com', 'Sheikh', 'Two', 0, '', strftime ('%s', '2021-05-12 09:54:01'), NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('patricia3@test.com', 'Patricia', 'Three', 1, 'PW', strftime ('%s', '2021-05-11 10:23:01'), strftime ('%s', '2021-05-11 10:23:01'))";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('elizabeth4@test.com', 'Elizabeth', 'Four', 1, 'PW', strftime ('%s', '2021-05-11 11:06:01'), strftime ('%s', '2021-05-11 11:06:01'))";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('jyoti5@test.com', 'Jyoti', 'Five', 0, '', strftime ('%s', '2021-05-12 12:08:01'), NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('edward6@test.com', 'Edward', 'Six', 1, 'PW', strftime ('%s', '2021-05-11 13:17:01'), strftime ('%s', '2021-05-11 13:17:01'))";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('graham7@test.com', 'Graham', 'Seven', 1, 'PW', strftime ('%s', '2021-05-11 13:17:01'), strftime ('%s', '2021-05-11 13:17:01'))";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('abigail8@test.com', 'Abigail', 'Eight', 0, '', strftime ('%s', '2021-05-11 13:17:01'), strftime ('%s', '2021-05-11 13:17:01'))";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('abdul9@test.com', 'Abdul', 'Nine', 0, '', strftime ('%s', '2021-05-11 13:17:01'), strftime ('%s', '2021-05-11 13:17:01'))";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Customers (EmailAddress, FirstName, Surname, IsRegistered, Password, CreatedDateTime, LastAccessedDateTime) VALUES('ling10@test.com', 'Ling', 'Ten', 0, '', strftime ('%s', '2021-05-11 13:17:01'), strftime ('%s', '2021-05-11 13:17:01'))";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateAddressesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Addresses(Id INTEGER PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER NOT NULL, Premise VARCHAR(100) NOT NULL, Street VARCHAR(100), District VARCHAR(100), City VARCHAR(100) NOT NULL, County VARCHAR(100), Postcode VARCHAR(15) NOT NULL, Phone VARCHAR(15) NOT NULL, IsDeliveryDefault BOOLEAN NOT NULL, IsBillingDefault BOOLEAN NOT NULL, DeliveryContact VARCHAR(100), DeliveryInstructions VARCHAR(100), FOREIGN KEY(CustomerId) REFERENCES Customers(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateAddressesTable(string connectionString)
        {
            //this is temporary test data
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(1, '12', 'Station Road', 'Lorem', 'Ipsum', 'Kent', 'ME4 6RD', '07777777789', 1, 1, '', 'If out, leave next to back gate')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(2, '6', 'Field View', '', 'Ipsum', '', 'PR6 0AT', '07777777790', 1, 1, '', '')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(3, 'The Dell', 'Salisbury Avenue', '', 'Aberdeen', '', 'AB10 1HH', '07777777791', 1, 1, '', '')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(4, '68a', 'Church Street', '', 'Enniskillen', 'Fermanagh', 'BT74 6AF', '07777777792', 1, 1, '', 'If out, leave with neighbour at number 70')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(5, 'End House', 'Great Ouse Lane', 'Orton Southgate', 'Peterborough', '', 'PE2 6YS', '07777777793', 1, 1, '', '')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(6, '142', 'London Road', 'Kennford', 'Exeter', 'Devon', 'EX6 7TP', '07777777794', 1, 1, 'Bill Jessop', '')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(7, 'Flat 11', 'School Lane', 'Camberwell', 'London', '', 'SE5 0XS', '07777777795', 1, 1, '', '')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(8, '12', 'Salem Street', 'Rutherglen', 'Glasgow', '', 'G73 3LW', '07777777796', 1, 1, '', '')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(9, '9', 'Creswell Place', '', 'London', '', 'SW10 9RD', '07777777797', 1, 1, '', '')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO Addresses (CustomerId, Premise, Street, District, City, County, Postcode, Phone, IsDeliveryDefault, IsBillingDefault, DeliveryContact, DeliveryInstructions) VALUES(10, '40', 'St Leonard''s Avenue', 'Blandford Forum', '', 'Dorset', 'DT11 7NY', '07777777798', 1, 1, '', '')";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateProductOptionTypesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE ProductOptionTypes(Id INTEGER PRIMARY KEY ASC, Name VARCHAR(20) NOT NULL)";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateProductOptionTypesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(1, 'N/A')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(2, 'Size')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(3, 'Age Range')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(4, 'Dress Size')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(5, 'Waist')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(6, 'Collar')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(7, 'Chest Size')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptionTypes VALUES(8, 'Shoe Size')";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateSupplierManufacturersTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE SupplierManufacturers(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(100) NOT NULL)";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateSupplierManufacturersTable(string connectionString)
        {
            //this is temporary test data
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Own Brand')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Lorem')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Ipsum')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Lorem Ipsum')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Dolor')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Sit')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Dolor Sit')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Amet')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Consectetur')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Amet Consectetur')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Elit')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Sed')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Elit Sed')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Do')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Eiusmod')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Do Eiusmod')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Tempor')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Incididunt')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SupplierManufacturers (Name) VALUES('Tempor Incididunt')";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateProductsTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Products(Id INTEGER PRIMARY KEY AUTOINCREMENT, Title VARCHAR(50) NOT NULL, FriendlyUrl VARCHAR(100) NOT NULL, Sku VARCHAR(20) NOT NULL, SupplierManufacturerId INTEGER, MainImageUrl VARCHAR(100) NOT NULL, MainImageThumbnailUrl VARCHAR(100) NOT NULL, SecondImageUrl VARCHAR(100), SecondImageThumbnailUrl VARCHAR(100), ThirdImageUrl VARCHAR(100), ThirdImageThumbnailUrl VARCHAR(100), FourthImageUrl VARCHAR(100), FourthImageThumbnailUrl VARCHAR(100), TrendingItemImageUrl VARCHAR(100) NOT NULL, RelatedImageUrl VARCHAR(100) NOT NULL, CategoryImageUrl VARCHAR(100) NOT NULL, CategoryHoverImageUrl VARCHAR(100) NOT NULL, CartThumbnailImageUrl VARCHAR(100) NOT NULL, BriefDescription TEXT NOT NULL, FullDescription TEXT NOT NULL, LeafCategoryId INTEGER NOT NULL, NumberOfTimesViewed INTEGER NOT NULL, ProductOptionTypeId INTEGER NOT NULL, FOREIGN KEY(SupplierManufacturerId) REFERENCES SupplierManufacturers(Id), FOREIGN KEY(LeafCategoryId) REFERENCES LeafCategories(Id), FOREIGN KEY(ProductOptionTypeId) REFERENCES ProductOptionTypes(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static string GetRandomLetters(int numberOfLetters)
        {
            var rnd = new Random();
            var randomString = string.Empty;
            for (var i = 1; i < (numberOfLetters + 1); i++)
            {
                randomString = randomString + ((char)rnd.Next('a','z')).ToString();
            }
            return randomString;
        }

        private static string GetRandomDigits(int numberOfDigits)
        {
            var rnd = new Random();
            var randomDigits = string.Empty;
            for (var i = 1; i < (numberOfDigits + 1); i++)
            {
                randomDigits = randomDigits + ((char)rnd.Next('0','9')).ToString();
            }
            return randomDigits;
        }

        private static void PopulateProductsTable(string connectionString)
        {
            //this is temporary test data

            const string imageUrl = "https://via.placeholder.com/555x510";
            const string thumbnailImageUrl = "https://via.placeholder.com/104x95";
            const string relatedImageUrl = "https://via.placeholder.com/255x348";
            const string trendingItemImageUrl = "https://via.placeholder.com/550x750";
            const string categoryImageUrl = "https://via.placeholder.com/262x359";
            const string categoryHoverImageUrl = "https://via.placeholder.com/262x328";
            const string cartThumbnailImageUrl = "https://via.placeholder.com/70x70";
            const string briefDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin commodo enim eget cursus interdum. Aliquam pellentesque blandit nibh, non maximus sem venenatis nec. Aliquam venenatis quam at ipsum malesuada rhoncus.";
            const string fullDescription = "__p__Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin commodo enim eget cursus interdum. Aliquam pellentesque blandit nibh, non maximus sem venenatis nec. Aliquam venenatis quam at ipsum malesuada rhoncus. Donec et nunc at felis blandit pellentesque. Proin consequat nisl sed pulvinar ullamcorper. Donec euismod malesuada neque, id laoreet leo porta at. Proin elementum tortor sed mollis fermentum. Vestibulum tincidunt venenatis metus eget interdum. Nam cursus aliquet quam.__cp__ __p__Vestibulum suscipit tortor eget fringilla molestie. Donec lacinia libero risus, eu feugiat diam congue ac. Sed id dui eget quam feugiat feugiat. Quisque ac odio non arcu euismod facilisis. Nulla facilisi. Duis vel commodo velit. Nullam faucibus, risus non porttitor congue, purus eros faucibus libero, quis sagittis ipsum ligula id felis. Etiam fermentum, massa vestibulum tempus sollicitudin, dui erat viverra ipsum, vel aliquam eros lectus in odio. Aenean et erat vel odio feugiat rhoncus. Curabitur vitae est ipsum. Ut fringilla felis sodales sem aliquam fringilla. Nulla vitae velit at urna ultricies condimentum. Nunc malesuada, mi et tristique facilisis, risus elit venenatis ipsum, non consectetur diam lorem vel dolor.__cp__";
            var rnd = new Random();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                for (var i = 1; i < 401; i++)
                {
                    var numberOfWordsInTitle = (rnd.Next(1, 3) == 3 ? rnd.Next(3, 5) : 2);
                    var word1Length = rnd.Next(3, 9);
                    var word2Length = rnd.Next(3, 9);
                    var word3Length = rnd.Next(3, 9);
                    var word4Length = rnd.Next(3, 9);
                    var productTitle = GetRandomLetters(8).Substring(0, word1Length);
                    var word2 = GetRandomLetters(8).Substring(0, word2Length);
                    var word3 = GetRandomLetters(8).Substring(0, word3Length);
                    var word4 = GetRandomLetters(8).Substring(0, word4Length);
                    productTitle = productTitle + " " + word2;
                    if (numberOfWordsInTitle >= 3)
                    {
                        productTitle = productTitle + " " + word3;
                    }
                    if (numberOfWordsInTitle == 4)
                    {
                        productTitle = productTitle + " " + word4;
                    }
                    var textInfo = new CultureInfo("en-GB", false).TextInfo;
                    productTitle = textInfo.ToTitleCase(productTitle);
                    var friendlyUrl = productTitle.ToLower().Replace(' ', '-');
                    var sku = GetRandomLetters(4).ToUpper() + GetRandomDigits(2);
                    var supplierManufacturerId = (rnd.Next(1, 4) == 3 ? rnd.Next(2, 20) : 1);
                    var leafCategoryId = rnd.Next(1, 189);
                    var numberOfTimesViewed = (rnd.Next(1, 4) == 3 ? rnd.Next(2, 10) : 1);
                    var productOptionTypeId = (rnd.Next(1, 4) == 3 ? rnd.Next(2, 9) : 1);
                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand();
                        insertCmd.CommandText ="INSERT INTO Products (Title, FriendlyUrl, Sku, supplierManufacturerId, MainImageUrl, MainImageThumbnailUrl, SecondImageUrl, SecondImageThumbnailUrl, ThirdImageUrl, ThirdImageThumbnailUrl, FourthImageUrl, FourthImageThumbnailUrl, TrendingItemImageUrl, RelatedImageUrl, CategoryImageUrl, CategoryHoverImageUrl, CartThumbnailImageUrl, BriefDescription, FullDescription, LeafCategoryId, NumberOfTimesViewed, ProductOptionTypeId) VALUES ('" + productTitle + "','" + friendlyUrl + "','" + sku + "'," + supplierManufacturerId + ",'" + imageUrl + "','" + thumbnailImageUrl + "','" + imageUrl + "','" + thumbnailImageUrl + "','" + imageUrl + "','" + thumbnailImageUrl + "','" + imageUrl + "','" + thumbnailImageUrl + "','" + trendingItemImageUrl + "','" + relatedImageUrl + "', '" + categoryImageUrl + "', '" + categoryHoverImageUrl + "', '" + cartThumbnailImageUrl + "', '" + briefDescription + "','" + fullDescription + "'," + leafCategoryId + "," + numberOfTimesViewed + "," + productOptionTypeId + ")";
                        insertCmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
            }
        }

        private static void CreateProductFeaturesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE ProductFeatures(Id INTEGER PRIMARY KEY AUTOINCREMENT, Feature VARCHAR(200) NOT NULL, ProductId INTEGER NOT NULL, FOREIGN KEY(ProductId) REFERENCES Products(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateProductFeaturesTable(string connectionString)
        {
            //this is temporary test data

            const string feature1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            const string feature2 = "Proin commodo enim eget cursus interdum";
            const string feature3 = "Aliquam pellentesque blandit nibh, non maximus sem venenatis nec";
            const string feature4 = "Aliquam venenatis quam at ipsum malesuada rhoncus";
            var rnd = new Random();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                for (var i = 1; i < 401; i++)
                {
                    var numberOfFeatures = rnd.Next(1, 5);
                    for (var j = 1; j < numberOfFeatures + 1; j++)
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            var insertCmd = connection.CreateCommand();
                            var commandText = "INSERT INTO ProductFeatures (Feature, ProductId) VALUES ('";
                            switch (j)
                            {
                                case 1:
                                    commandText = commandText + feature1;
                                    break;
                                case 2:
                                    commandText = commandText + feature2;
                                    break;
                                case 3:
                                    commandText = commandText + feature3;
                                    break;
                                case 4:
                                    commandText = commandText + feature4;
                                    break;
                            }
                            commandText = commandText + "'," + i + ")";
                            insertCmd.CommandText = commandText;
                            insertCmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
            }
        }

        private static void CreateProductOptionsTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE ProductOptions(Id INTEGER PRIMARY KEY ASC, Option VARCHAR(50) NOT NULL, ProductOptionTypeId INTEGER NOT NULL, SalesTaxTypeId INTEGER NOT NULL, FOREIGN KEY(ProductOptionTypeId) REFERENCES ProductOptionTypes(Id), FOREIGN KEY(SalesTaxTypeId) REFERENCES SalesTaxTypes(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateProductOptionsTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(1, 'N/A', 1, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(2, 'XXS', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(3, 'XS', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(4, 'Small', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(5, 'Medium', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(6, 'Large', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(7, 'XL', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(8, 'XXL', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(9, 'XXXL', 2, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(10, 'Premature', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(11, 'Newborn', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(12, '0 - 3 months', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(13, '3 - 6 months', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(14, '6 - 9 months', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(15, '9 - 12 months', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(16, '12 - 18 months', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(17, '18 - 24 months', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(18, '2 - 3 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(19, '3 - 4 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(20, '4 - 5 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(21, '5 - 6 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(22, '6 - 7 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(23, '7 - 8 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(24, '8 - 9 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(25, '9 - 10 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(26, '10 - 11 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(27, '11 - 12 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(28, '12 - 13 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(29, '13 - 14 years', 3, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(30, '2', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(31, '4', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(32, '6', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(33, '8', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(34, '10', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(35, '12', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(36, '14', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(37, '16', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(38, '18', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(39, '20', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(40, '22', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(41, '24', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(42, '26', 4, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(43, '24', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(44, '26', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(45, '28', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(46, '30', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(47, '32', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(48, '34', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(49, '36', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(50, '38', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(51, '40', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(52, '42', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(53, '44', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(54, '46', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(55, '48', 5, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(56, '13″ - 14″', 6, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(57, '14″ - 15″', 6, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(58, '15″ - 16″', 6, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(59, '16″ - 17″', 6, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(60, '17″ - 18″', 6, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(61, '18″ - 19″', 6, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(62, '19″ - 20″', 6, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(63, '30″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(64, '32″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(65, '34″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(66, '36″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(67, '38″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(68, '40″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(69, '42″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(70, '44″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(71, '46″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(72, '48″', 7, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(73, 'UK 2 - EU 17½', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(74, 'UK 3 - EU 18½', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(75, 'UK 4 - EU 20', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(76, 'UK 5 - EU 21', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(77, 'UK 6 - EU 22½', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(78, 'UK 7 - EU 24', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(79, 'UK 8 - EU 25½', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(80, 'UK 9 - EU 27', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(81, 'UK 10 - EU 28', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(82, 'UK 11 - EU 29', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(83, 'UK 12 - EU 30', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(84, 'UK 13 - EU 32', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(85, 'UK 1 - EU 33', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(86, 'UK 2 - EU 34', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(87, 'UK 3 - EU 35½', 8, 3)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(88, 'UK 4 - EU 37', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(89, 'UK 5 - EU 38', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(90, 'UK 6 - EU 39½', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(91, 'UK 7 - EU 41', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(92, 'UK 8 - EU 42', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(93, 'UK 9 - EU 43', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(94, 'UK 10 - EU 44½', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(95, 'UK 11 - EU 46', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(96, 'UK 12 - EU 47', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO ProductOptions VALUES(97, 'UK 13 - EU 48', 8, 1)";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateProductOptionProductsTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE ProductOptionProducts(Id INTEGER PRIMARY KEY AUTOINCREMENT, ProductOptionId INTEGER NOT NULL, ProductId INTEGER NOT NULL, Stock INTEGER NOT NULL, FOREIGN KEY(ProductOptionId) REFERENCES ProductOptions(Id), FOREIGN KEY(ProductId) REFERENCES Products(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateProductOptionProductsTable(string connectionString)
        {
            //this is temporary test data
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                for (var i = 1; i < 401; i++)
                {
                    string stm = "SELECT ProductOptionTypeId FROM Products WHERE Id = " + i + ";";
                    int productOptionTypeId = 1;

                    using (var cmd = new SqliteCommand(stm, connection))
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                productOptionTypeId = rdr.GetInt32(rdr.GetOrdinal("ProductOptionTypeId"));
                            }
                        }
                    }

                    stm = "SELECT Id FROM ProductOptions WHERE ProductOptionTypeId = " + productOptionTypeId + ";";

                    using (var cmd = new SqliteCommand(stm, connection))
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var productOptionId = rdr.GetInt32(rdr.GetOrdinal("Id"));
                                using (var transaction = connection.BeginTransaction())
                                {
                                    var insertCmd = connection.CreateCommand();
                                    insertCmd.CommandText = "INSERT INTO ProductOptionProducts (ProductOptionId, ProductId, Stock) VALUES(" + productOptionId + ", " + i + ", 100)";
                                    insertCmd.ExecuteNonQuery();
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CreateSalesTaxTypesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE SalesTaxTypes(Id INTEGER PRIMARY KEY ASC, Name VARCHAR(20) NOT NULL)";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateSalesTaxTypesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO SalesTaxTypes VALUES(1, 'Standard rate')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SalesTaxTypes VALUES(2, 'Reduced rate')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SalesTaxTypes VALUES(3, 'Zero rate')";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateSalesTaxesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE SalesTaxes(Id INTEGER PRIMARY KEY ASC, SalesTaxTypeId INTEGER NOT NULL, Amount REAL NOT NULL, EffectiveFromDate INTEGER NOT NULL, EffectiveToDate INTEGER)";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateSalesTaxesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO SalesTaxes VALUES(1, 1, 20.0, strftime ('%s', '2011-01-04 00:00:01'), NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SalesTaxes VALUES(2, 2, 5.0, strftime ('%s', '1997-09-01 00:00:01'), NULL)";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO SalesTaxes VALUES(3, 3, 0.0, strftime ('%s', '1973-01-01 00:00:01'), NULL)";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private static void CreateProductOptionProductInstancesTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE ProductOptionProductInstances(Id INTEGER PRIMARY KEY AUTOINCREMENT, ProductOptionProductId INTEGER NOT NULL, Price REAL NOT NULL, EffectiveFromDate INTEGER NOT NULL, EffectiveToDate INTEGER, FOREIGN KEY(ProductOptionProductId) REFERENCES ProductOptionProducts(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateProductOptionProductInstancesTable(string connectionString)
        {
            //this is temporary test data
            var rnd = new Random();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                for (var i = 1; i < 401; i++)
                {
                    var stm = "SELECT Id FROM ProductOptionProducts WHERE ProductId = " + i + ";";
                    var pounds = rnd.Next(0, 26);
                    var pence = rnd.Next(0, 100);
                    if (pounds == 0 && pence == 0)
                    {
                        pence = 99;
                    }

                    var poundsAndPenceString = "" + pounds + ".";
                    if (pence < 10)
                    {
                        poundsAndPenceString = poundsAndPenceString + "0" + pence;
                    }
                    else {
                        poundsAndPenceString = poundsAndPenceString + pence;
                    }
                    var poundsAndPence = Decimal.Parse(poundsAndPenceString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                    using (var cmd = new SqliteCommand(stm, connection))
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var productOptionProductId = rdr.GetInt32(rdr.GetOrdinal("Id"));
                                using (var transaction = connection.BeginTransaction())
                                {
                                    var insertCmd = connection.CreateCommand();
                                    insertCmd.CommandText = "INSERT INTO ProductOptionProductInstances (ProductOptionProductId, Price, EffectiveFromDate, EffectiveToDate) VALUES(" + productOptionProductId + ", " + poundsAndPence + ", strftime ('%s', '2021-04-28 00:00:01'), NULL)";
                                    insertCmd.ExecuteNonQuery();
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CreateOrdersTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Orders(Id INTEGER PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER NOT NULL, BillingAddressId INTEGER NOT NULL, DeliveryAddressId INTEGER NOT NULL, ProductTotal REAL NOT NULL, DeliveryCharge REAL NOT NULL, SalesTaxTotal REAL NOT NULL, Total REAL NOT NULL, PaymentStatus INTEGER NOT NULL, CreatedDateTime INTEGER NOT NULL, FOREIGN KEY(CustomerId) REFERENCES Customers(Id), FOREIGN KEY(BillingAddressId) REFERENCES Addresses(Id), FOREIGN KEY(DeliveryAddressId) REFERENCES Addresses(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void CreateOrderDetailsTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE OrderDetails(Id INTEGER PRIMARY KEY AUTOINCREMENT, OrderId INTEGER NOT NULL, ProductOptionProductInstanceId INTEGER NOT NULL, Quantity INTEGER NOT NULL, ProductTotal REAL NOT NULL, SalesTaxTotal REAL NOT NULL, FOREIGN KEY(OrderId) REFERENCES Orders(Id), FOREIGN KEY(ProductOptionProductInstanceId) REFERENCES ProductOptionProductInstances(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void PopulateOrdersAndOrderDetailsTable(string connectionString)
        {
            //this is temporary test data
            var rnd = new Random();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                for (var customerId = 1; customerId < 11; customerId++)
                {
                    //3. Determine random number of orders per customer
                    var numberOfOrders = rnd.Next(1, 3) == 2 ? rnd.Next(2, 8) : 1;
                    
                    for (var orderNumber = 1; orderNumber < numberOfOrders + 1; orderNumber++)
                    {
                        //4. Determine random number of products ordered by customer
                        var numberOfProducts = rnd.Next(1, 3) == 2 ? rnd.Next(2, 6) : 1;
                        var orderDetails = new List<OrderDetail>();
                        var stm = string.Empty;

                        for (var product = 1; product < numberOfProducts + 1; product++)
                        {
                            var productId = rnd.Next(1, 401);
                            var orderDetail = new OrderDetail();
                            var minProductOptionId = 0;
                            var maxProductOptionId = 0;
                            var salesTaxAmount = 0.0m;
                            var productSalesTaxAmount = 0.0m;
                            var productPrice = 0.0m;
                            var productOptionProductInstanceId = 0;

                            stm = "SELECT MIN(pop.ProductOptionId) AS MinProductOptionId, MAX(pop.ProductOptionId) AS MaxProductOptionId FROM ProductOptionProducts pop WHERE pop.ProductId = " + productId + ";";
                            using (var cmd = new SqliteCommand(stm, connection))
                            {
                                using (var rdr = cmd.ExecuteReader())
                                {
                                    while (rdr.Read())
                                    {
                                        minProductOptionId = rdr.GetInt32(rdr.GetOrdinal("MinProductOptionId"));
                                        maxProductOptionId = rdr.GetInt32(rdr.GetOrdinal("MaxProductOptionId"));
                                    }
                                }
                            }

                            //5. Pick a random ProductOptionId
                            var productOptionId = rnd.Next(minProductOptionId, maxProductOptionId + 1);

                            //6. Get sales tax amount
                            stm = "SELECT st.Amount FROM SalesTaxes st INNER JOIN ProductOptions po ON st.SalesTaxTypeId = po.SalesTaxTypeId WHERE po.Id = " + productOptionId + " AND st.EffectiveFromDate <= strftime ('%s', 'now') AND (st.EffectiveToDate IS NULL OR st.EffectiveToDate > strftime ('%s', 'now'));";
                            using (var cmd = new SqliteCommand(stm, connection))
                            {
                                using (var rdr = cmd.ExecuteReader())
                                {
                                    while (rdr.Read())
                                    {
                                        salesTaxAmount = rdr.GetDecimal(rdr.GetOrdinal("Amount"));
                                    }
                                }
                            }

                            //7. Get ProductOptionProductInstanceId and ProductPrice
                            stm = "SELECT popi.Id, popi.Price FROM ProductOptionProducts pop INNER JOIN ProductOptionProductInstances popi ON pop.Id = popi.ProductOptionProductId WHERE pop.ProductId = " + productId + " AND pop.ProductOptionId = " + productOptionId + " AND popi.EffectiveFromDate <= strftime ('%s', 'now') AND (popi.EffectiveToDate IS NULL OR popi.EffectiveToDate > strftime ('%s', 'now'));";
                            using (var cmd = new SqliteCommand(stm, connection))
                            {
                                using (var rdr = cmd.ExecuteReader())
                                {
                                    while (rdr.Read())
                                    {
                                        productOptionProductInstanceId = rdr.GetInt32(rdr.GetOrdinal("Id"));
                                        productPrice = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                                    }
                                }
                            }

                            //8. Determine random quantity of product ordered
                            var qty = (rnd.Next(1, 4) == 3 ? rnd.Next(2, 4) : 1);
                            
                            //9. Calculate sales tax
                            productSalesTaxAmount = salesTaxAmount * 0.01m;
                            
                            orderDetail.ProductOptionProductInstanceId = productOptionProductInstanceId;
                            orderDetail.Quantity = qty;
                            orderDetail.SalesTaxTotal = Math.Round(productPrice * productSalesTaxAmount, 2, MidpointRounding.AwayFromZero) * qty;
                            orderDetail.ProductTotal = productPrice * qty;
                            orderDetails.Add(orderDetail);
                        }

                        //10. calculate total cost of order and add delivery charge of £5 if order is below £100
                        var productTotal = orderDetails.Sum(od => od.ProductTotal);
                        var deliveryCharge = productTotal > 50.0m ? 5.0m : 0.0m;
                        var salesTaxTotal = (0.2m * deliveryCharge) + orderDetails.Sum(od => od.SalesTaxTotal);
                        var total = (deliveryCharge + (0.2m * deliveryCharge)) + productTotal + orderDetails.Sum(od => od.SalesTaxTotal);

                        //11. populate Orders table
                        var insertCmd = connection.CreateCommand();
                        insertCmd.CommandText = "INSERT INTO Orders (CustomerId, BillingAddressId, DeliveryAddressId, ProductTotal, DeliveryCharge, SalesTaxTotal, Total, PaymentStatus, CreatedDateTime) VALUES (" + customerId + ", " + customerId + ", " + customerId + ", " + productTotal + ", " + deliveryCharge + ", " + salesTaxTotal + ", " + total + ", 3, strftime ('%s', 'now'));";
                        insertCmd.ExecuteNonQuery();

                        //12. Retrieve the newly created orderid
                        var orderId = 0;
                        stm = "SELECT Id FROM Orders WHERE CustomerId = " + customerId + " AND ProductTotal = " + productTotal + " AND Total = " + total + ";";
                        using (var cmd = new SqliteCommand(stm, connection))
                        {
                            using (var rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    orderId = rdr.GetInt32(rdr.GetOrdinal("Id"));
                                }
                            }
                        }

                        //13. populate OrderDetails table
                        foreach (var orderDetail in orderDetails)
                        {
                            insertCmd.CommandText = "INSERT INTO OrderDetails (OrderId, ProductOptionProductInstanceId, Quantity, ProductTotal, SalesTaxTotal) VALUES (" + orderId + ", " + orderDetail.ProductOptionProductInstanceId + ", " + orderDetail.Quantity + ", " + orderDetail.ProductTotal + ", " + orderDetail.SalesTaxTotal + ");";
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private static void CreateCartsTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Carts(Id INTEGER PRIMARY KEY AUTOINCREMENT, Guid VARCHAR(36) NOT NULL, CreatedDateTime INTEGER NOT NULL)";
                createTableCmd.ExecuteNonQuery();
            }
        }

        private static void CreateCartItemsTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE CartItems(Id INTEGER PRIMARY KEY AUTOINCREMENT, CartId INTEGER NOT NULL, ProductOptionProductInstanceId INTEGER NOT NULL, Quantity INTEGER NOT NULL, FOREIGN KEY(CartId) REFERENCES Carts(Id), FOREIGN KEY(ProductOptionProductInstanceId) REFERENCES ProductOptionProductInstances(Id))";
                createTableCmd.ExecuteNonQuery();
            }
        }
    }

    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductOptionProductInstanceId { get; set; }
        public int Quantity { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal SalesTaxTotal { get; set; }
    }
}