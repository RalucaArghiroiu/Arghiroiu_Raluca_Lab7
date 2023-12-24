using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Arghiroiu_Raluca_Lab7.Models;

namespace Arghiroiu_Raluca_Lab7.Data
{
    public class ShoppingListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        
        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
            _database.CreateTableAsync<Shop>().Wait();
        }

        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }

        public Task<ShopList> GetShopListAsync(int id)
        {
            return _database.Table<ShopList>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveShopListAsync(ShopList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }

        public Task<int> DeleteShopListAsync(ShopList slist)
        {
            return _database.DeleteAsync(slist);
        }

        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveListProductAsync(ListProduct listProduct)
        {
            if (listProduct.ID != 0)
            {
                return _database.UpdateAsync(listProduct);
            }
            else
            {
                return _database.InsertAsync(listProduct);
            }
        }

        public Task<List<Product>> GetListProductsAsync(int shopListID)
        {
            return _database.QueryAsync<Product>(
                "SELECT p.ID, p.Description " +
                "FROM Product p " +
                "JOIN ListProduct lp ON lp.ProductID = p.ID " +
                "WHERE lp.ShopListID = ?",
                shopListID
            );
        }

        public Task<int> DeleteListProductAsync(int shopListID, int productID)
        {
            return _database.ExecuteAsync(
                "DELETE FROM ListProduct " +
                "WHERE ShopListID = ? AND ProductID = ?",
                shopListID,
                productID
            );
        }

        public Task<List<Shop>> GetShopsAsync()
        {
            return _database.Table<Shop>().ToListAsync();
        }

        public Task<int> SaveShopAsync(Shop shop)
        {
            if (shop.ID != 0)
            {
                return _database.UpdateAsync(shop);
            }
            else
            {
                return _database.InsertAsync(shop);
            }
        }

        public Task<int> DeleteShopAsync(Shop shop)
        {
            return _database.DeleteAsync(shop);
        }
    }
}
