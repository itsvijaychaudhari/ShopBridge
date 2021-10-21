using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Data;
using ShopBridge.Entities;
using ShopBridge.Interfaces;

namespace ShopBridge.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DataContext dbContext;

        public InventoryRepository(DataContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Inventory> AddItem(Inventory inventory)
        {
            var item = await dbContext.Inventories.AddAsync(inventory);
            await dbContext.SaveChangesAsync();
            return item.Entity;
        }

        public async Task DeleteItem(int itemId)
        {
            var item = await dbContext.Inventories.FirstOrDefaultAsync(x => x.Id == itemId);
            if (item != null)
            {
                dbContext.Inventories.Remove(item);
                await dbContext.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Inventory>> GetItems()
        {
            return await dbContext.Inventories.ToListAsync();
        }

        public async Task<Inventory> SearchById(int itemId)
        {
            return await dbContext.Inventories.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<Inventory> SearchByName(string name)
        {
            return await dbContext.Inventories.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Inventory> UpdateItem(Inventory inventory)
        {
            var item = dbContext.Inventories.FirstOrDefaultAsync(x => x.Id == inventory.Id);
            if (item != null)
            {
                item.Result.Name = inventory.Name;
                item.Result.Description = inventory.Description;
                item.Result.Price = inventory.Price;
                item.Result.Quantity = inventory.Quantity;

                await dbContext.SaveChangesAsync();
                return item.Result;
            }

            return null;
        }
    }
}
