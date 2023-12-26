using System.Collections.Generic;
using System.Threading.Tasks;
using RaythosAerospaceMVC.Models;
using Stripe.Climate;

namespace RaythosAerospaceMVC.Repositories
{
    public interface IOrderRepository
    {
        Task<List<ManufacturingProgress>> GetAllAsync(int userId);
        Task<ManufacturingProgress> GetByIdAsync(int id);
        Task AddAsync(ManufacturingProgress order);
        Task UpdateAsync(ManufacturingProgress order);
        Task DeleteAsync(ManufacturingProgress order);
    }
}
