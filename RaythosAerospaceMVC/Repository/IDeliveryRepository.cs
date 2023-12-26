using System.Collections.Generic;
using System.Threading.Tasks;
using RaythosAerospaceMVC.Models;
using Stripe.Climate;

namespace RaythosAerospaceMVC.Repositories
{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> GetAllAsync();
        Task<Delivery> GetByIdAsync(int id);
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task DeleteAsync(Delivery delivery);
    }
}
