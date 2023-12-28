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

            if (existingInventory != null)
            {

                // Update existing Delivery
                existingInventory.InventorySeats = inventory.InventorySeats;
                existingInventory.InventoryEngines = inventory.InventoryEngines;
                existingInventory.InventoryAircraftBody = inventory.InventoryAircraftBody;
                existingInventory.InventoryAirframes = inventory.InventoryAirframes;
                existingInventory.InventoryAvionicsSystems = inventory.InventoryAvionicsSystems;
                existingInventory.InventoryFuelTanks = inventory.InventoryFuelTanks;
                existingInventory.InventoryDescription = inventory.InventoryDescription;

                existingInventory.UpdatedDate = DateTime.Now;

                // Update other properties as needed
                await _context.SaveChangesAsync();
            }

            var Aircraft = await _context.Aircrafts.FindAsync(inventory.AircraftId);

            if (Aircraft != null)
            {

                // Update existing Delivery
                Aircraft.InventoryEngines = inventory.InventoryEngines;
                Aircraft.UpdatedDate = DateTime.Now;

                // Update other properties as needed
                await _context.SaveChangesAsync();
            }
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

        public async Task<IQueryable<Inventory>> GetInventoriesByDateRange(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null || toDate == null)
            {
                // Handle null values appropriately
                return Enumerable.Empty<Inventory>().AsQueryable();
            }

            return _context.Inventory
                .Where(inventory => inventory.CreatedDate >= fromDate && inventory.CreatedDate <= toDate);
        }

    }
}
