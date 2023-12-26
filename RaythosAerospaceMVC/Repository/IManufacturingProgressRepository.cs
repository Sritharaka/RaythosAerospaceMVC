using System.Collections.Generic;
using System.Threading.Tasks;
using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repositories
{
    public interface IManufacturingProgressRepository
    {
        Task<List<ManufacturingProgress>> GetAllAsync();
        Task<ManufacturingProgress> GetByIdAsync(int id);
        Task AddAsync(ManufacturingProgress progress);
        Task UpdateAsync(ManufacturingProgress progress);
        Task DeleteAsync(ManufacturingProgress progress);
    }
}
