using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetPaymentByUserId(int userId);
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment> UpdatePayment(Payment payment);
        Task DeletePayment(int id);
        //Task<string> GetCustomerName(string customerId);
    }
}
