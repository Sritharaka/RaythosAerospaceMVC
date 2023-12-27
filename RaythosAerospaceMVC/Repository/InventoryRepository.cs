using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AerospaceDbContext _context;

        public InventoryRepository(AerospaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetAllInventoriesAsync()
        {
            return await _context.Inventory.ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            return await _context.Inventory.FirstOrDefaultAsync(i => i.InventoryId == inventoryId);
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            var existingInventory = await _context.Inventory.FindAsync(inventory.InventoryId);

            if (existingInventory == null)
            {
                throw new InvalidOperationException("Inventory not found");
            }

            // Update properties of existingInventory with new values from inventory
            existingInventory.ModelName = inventory.ModelName;
            existingInventory.Manufacturer = inventory.Manufacturer;
            existingInventory.Description = inventory.Description;
            existingInventory.SeatingType = inventory.SeatingType;
            existingInventory.InteriorDesign = inventory.InteriorDesign;
            existingInventory.ImageUrl = inventory.ImageUrl;
            existingInventory.BasePrice = inventory.BasePrice;
            existingInventory.EngineType = inventory.EngineType;
            existingInventory.MaximumSpeed = inventory.MaximumSpeed;
            existingInventory.SeatingCapacity = inventory.SeatingCapacity;
            existingInventory.FuelCapacity = inventory.FuelCapacity;
            existingInventory.Weight = inventory.Weight;
            existingInventory.UpdatedDate = inventory.UpdatedDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _context.Inventory.FindAsync(inventoryId);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> UploadImage(IFormFile imageFile)
        {
            // Same as before, no changes needed for image upload logic
            // ...
            return null;
        }
    }
}
