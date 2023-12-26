using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;

namespace RaythosAerospaceMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }


        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

                    contact.UserId = userId;
                    contact.CreatedDate = DateTime.Now;

                    var createdContact = await _contactRepository.CreateContact(contact);
                    return RedirectToAction("Contact", "Contact");                    // Redirect to a contact details page after creating the contact
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(contact); // If the model state is invalid, return to the contact creation view
        }

    }
}
