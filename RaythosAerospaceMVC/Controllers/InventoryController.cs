using Microsoft.AspNetCore.Mvc;
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

    }
}
