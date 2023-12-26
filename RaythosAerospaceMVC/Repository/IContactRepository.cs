using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repository
{
    public interface IContactRepository
    {
        Task<Contact> CreateContact(Contact contact);

    }
}
