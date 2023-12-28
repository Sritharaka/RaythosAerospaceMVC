using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;
using X.PagedList;

namespace RaythosAerospaceMVC.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryRepository aircraftRepository, ILogger<InventoryController> logger)
        {
            _inventoryRepository = aircraftRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Inventory()
        {
            //var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();
            //return View(aircrafts);

            return View();

        }

        public async Task<IActionResult> _DisplayInventoryTable(int? page)
        {
            int pageNumber = page ?? 1; // If no page is specified, default to page 1
            int pageSize = 10; // Number of items per page

            var aircrafts = await _inventoryRepository.GetAllInventoriesAsync();

            // Return a paged list
            var pagedAircrafts = aircrafts.ToPagedList(pageNumber, pageSize);

            return PartialView("_DisplayInventoryTable", pagedAircrafts);
        }


        public async Task<IActionResult> Edit(int inventoryId)
        {
            var inventory = await _inventoryRepository.GetInventoryByIdAsync(inventoryId);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Aircrafts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("InventoryId, ModelName, Manufacturer, Description, " +
            "BasePrice, AirframeProgress, AvionicsSystemsProgress, EnginesProgress, InteriorProgress," +
            " OverallProgress, DeliveryDate, Telephone, EmailAddress," +
            " CardHolderName, InventorySeats, InventoryEngines, InventoryAircraftBody, InventoryAirframes, InventoryAvionicsSystems, InventoryFuelTanks, InventoryDescription, AircraftId"
            )] Inventory inventory)
        {
            //if (id != aircraft.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var existingInventory = await _inventoryRepository.GetInventoryByIdAsync(inventory.InventoryId);
                if (existingInventory == null)
                {
                    return NotFound();
                }

                try
                {



                    // Set the ImageUrl property of the aircraft
                    inventory.UpdatedDate = DateTime.Now;

                    await _inventoryRepository.UpdateInventoryAsync(inventory);
                    _logger.LogInformation($"Aircraft with ID {inventory.InventoryId} updated at {DateTime.Now}");
                    return RedirectToAction("Inventory", "Inventory"); // Redirect to Home/Index upon successful login

                    //return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return BadRequest(); // Or handle the exception accordingly
                }
            }
            return View(inventory);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var aircraft = await _inventoryRepository.GetInventoryByIdAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _inventoryRepository.DeleteInventoryAsync(id);
            _logger.LogInformation($"Aircraft with ID {id} deleted at {DateTime.Now}");
            return RedirectToAction("Aircraft", "Aircraft");
        }




    }


}
