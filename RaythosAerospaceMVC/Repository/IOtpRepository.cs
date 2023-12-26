using RaythosAerospaceMVC.Models;
using static System.Net.WebRequestMethods;

namespace RaythosAerospaceMVC.Repository
{
    public interface IOtpRepository
    {
        Task<bool> SendOtpAsync(string Telephone, string otp);
        Task<Otps> SaveOtpAsync(Otps otp);
    }
}
