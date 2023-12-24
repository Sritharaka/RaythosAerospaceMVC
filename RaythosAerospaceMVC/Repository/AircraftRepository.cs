
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaythosAerospaceMVC.Models;


namespace RaythosAerospaceMVC.Repository
{

    public class AircraftRepository : IAircraftRepository
    {
        private readonly AerospaceDbContext _context;

        public AircraftRepository(AerospaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Aircraft>> GetAllAircraftsAsync()
        {
            return await _context.Aircrafts.ToListAsync();
        }

        public async Task<Aircraft> GetAircraftByIdAsync(int aircraftId)
        {
            return await _context.Aircrafts.FirstOrDefaultAsync(a => a.Id == aircraftId);
        }

        public async Task AddAircraftAsync(Aircraft aircraft)
        {
            _context.Aircrafts.Add(aircraft);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAircraftAsync(Aircraft aircraft)
        {
            var existingAircraft = await _context.Aircrafts.FindAsync(aircraft.Id);

            if (existingAircraft == null)
            {
                throw new InvalidOperationException("Aircraft not found");
            }
            //var newAircraft = new Aircraft
            //{
            existingAircraft.ModelName = aircraft.ModelName;
            existingAircraft.Manufacturer = aircraft.Manufacturer;
            existingAircraft.Description = aircraft.Description;
            existingAircraft.SeatingType = aircraft.SeatingType;
            existingAircraft.InteriorDesign = aircraft.InteriorDesign;
            existingAircraft.ImageUrl = aircraft.ImageUrl;
            existingAircraft.BasePrice = aircraft.BasePrice;
            existingAircraft.EngineType = aircraft.EngineType;
            existingAircraft.MaximumSpeed = aircraft.MaximumSpeed;
            existingAircraft.SeatingCapacity = aircraft.SeatingCapacity;
            existingAircraft.FuelCapacity = aircraft.FuelCapacity;
            existingAircraft.Weight = aircraft.Weight;
            existingAircraft.UpdatedDate = aircraft.UpdatedDate;



            // Copy other properties as needed
            //};

            // Update the properties of the existingAircraft entity with the new values
            //_context.Entry(existingAircraft).CurrentValues.SetValues(newAircraft);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAircraftAsync(int aircraftId)
        {
            var aircraft = await _context.Aircrafts.FindAsync(aircraftId);
            if (aircraft != null)
            {
                _context.Aircrafts.Remove(aircraft);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                throw new ArgumentException("Image file is empty or null.");
            }

            try
            {
                // Generate a unique file name or use a GUID
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                // Define the directory to save the uploaded file
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "aircraftImages");

                // Ensure the directory exists, create if not
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Combine the directory path and file name
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Copy the file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Return the relative path to the uploaded file
                return Path.Combine("aircraftImages", uniqueFileName);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (logging, error handling, etc.)
                throw new ApplicationException("Error occurred while uploading the image.", ex);
            }
        }
    }


}
