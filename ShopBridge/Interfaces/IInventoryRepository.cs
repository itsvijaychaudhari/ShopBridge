using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopBridge.Entities;

namespace ShopBridge.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory> AddItem(Inventory inventory);
        Task<Inventory> UpdateItem(Inventory inventory);
        Task DeleteItem(int itemId);
        Task<IEnumerable<Inventory>> GetItems();

        Task<Inventory> SearchById(int itemId);
        Task<Inventory> SearchByName(string name);

    }
}
