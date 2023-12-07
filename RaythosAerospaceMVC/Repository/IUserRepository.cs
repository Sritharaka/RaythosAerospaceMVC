using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repository
{
    public interface IUserRepository
    {
        string AddUser(Models.User newUser);
        User AuthenticateUser(string email, string password);
        bool IsEmailAlreadyExists(string email);
    }
}
