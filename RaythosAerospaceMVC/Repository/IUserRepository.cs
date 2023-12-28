using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repository
{
    public interface IUserRepository
    {
        string AddUser(Models.User newUser);
        User AuthenticateUser(string email, string password);
        User GetUser(string email);
        Task<User> GetUserByIdAsync(int id);
        bool IsEmailAlreadyExists(string email);

        Task<string> UploadImage(IFormFile imageFile);
        Task<bool> UpdateUser(User updatedUser);
        Task<List<User>> GetUserList();
        Task UpdateAsync(User user);

        Task DeleteUserAsync(int id);


    }
}
