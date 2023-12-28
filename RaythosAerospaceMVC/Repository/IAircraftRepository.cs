using RaythosAerospaceMVC.Models;

namespace RaythosAerospaceMVC.Repository
{
    public interface IAircraftRepository
    {
        Task<List<Aircraft>> GetAllAircraftsAsync();
        Task<Aircraft> GetAircraftByIdAsync(int aircraftId);
        Task AddAircraftAsync(Aircraft aircraft);
        Task UpdateAircraftAsync(Aircraft aircraft);
        Task DeleteAircraftAsync(int aircraftId);
        Task<string> UploadImage(IFormFile imageFile);

        Task<IQueryable<Aircraft>> GetAircraftsByDateRange(DateTime? fromDate, DateTime? toDate);


    }
}
