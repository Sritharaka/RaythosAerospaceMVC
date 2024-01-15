using Microsoft.AspNetCore.Mvc;

namespace RaythosAerospaceMVC.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetBotResponse(string userMessage)
        {
            // Prices for each parameter
            var priceChangeInteriorDesign = 0;
            var priceChangeSeatingCapacity = 0;
            var priceChangeSeatingType = 0;
            var priceChangeFuelCapacity = 0;
            var priceChangeImageFile = 0;

            // Perform your conditions and set the price changes accordingly
            switch (userMessage.ToLower())
            {
                case "increase seating capacity by 55":
                    priceChangeSeatingCapacity = 5000;
                    return Json($"Change Seating Capacity price as {priceChangeSeatingCapacity} USD.");

                case "decrease seating capacity by 55":
                    priceChangeSeatingCapacity = -3000;
                    return Json($"Change Seating Capacity price as {priceChangeSeatingCapacity} USD.");

                case "increase seating capacity by 20":
                    priceChangeSeatingCapacity = 2000;
                    return Json($"Change Seating Capacity price as {priceChangeSeatingCapacity} USD.");

                case "semi luxury interior design":
                    priceChangeInteriorDesign = -2000;
                    return Json($"Change Interior Design price as {priceChangeInteriorDesign} USD.");

                case "super luxury interior design":
                    priceChangeInteriorDesign = 2000;
                    return Json($"Change Interior Design price as {priceChangeInteriorDesign} USD.");

                case "luxury interior design":
                    priceChangeInteriorDesign = 500;
                    return Json($"Change Interior Design price as {priceChangeInteriorDesign} USD.");

                case "economy seating type":
                    priceChangeSeatingType = -5000;
                    return Json($"Change Seating Type price as {priceChangeSeatingType} USD.");

                case "business seating type":
                    priceChangeSeatingType = 3000;
                    return Json($"Change Seating Type price as {priceChangeSeatingType} USD.");

                case "first class seating type":
                    priceChangeSeatingType = 5000;
                    return Json($"Change Seating Type price as {priceChangeSeatingType} USD.");

                case "design changes for plane":
                    priceChangeImageFile = 5000;
                    return Json($"Change Image File price as {priceChangeImageFile} USD.");

                // Add other cases for different commands
                case "some other command":
                    return Json("You've entered some other command.");

                // Add more cases as needed for different commands
                // Add more cases for common questions
                case "what is the range of this plane?":
                    return Json("The range of this plane is [insert range]. It represents the maximum distance the aircraft can travel without refueling.");

                case "tell me about the fuel capacity.":
                    return Json("The fuel capacity of this plane is [insert fuel capacity]. It indicates the maximum amount of fuel the aircraft can carry.");

                case "how can I enhance the fuel efficiency of the plane?":
                    return Json("To enhance the fuel efficiency of the plane, consider options such as selecting a more fuel-efficient seating type or exploring fuel-saving design changes.");

                case "what safety features does this plane have?":
                    return Json("This plane is equipped with advanced safety features, including [insert safety features]. These features ensure a secure and reliable flying experience.");

                case "are there any eco-friendly options available?":
                    return Json("Yes, we offer eco-friendly options for this plane. You can choose environmentally conscious interior materials and fuel-efficient configurations.");

                // Add more cases as needed for different questions

                // Common questions about planes
                case "what is the seating capacity of this plane?":
                    return Json("The seating capacity of this plane is defined Aircraft Catelog");

                case "tell me about the interior design options":
                    return Json("We offer various interior design options, including Semi Luxury, Super Luxury, and Luxury. Each option comes with unique features and pricing.");

                case "what seating types are available?":
                    return Json("We provide three seating types: Economy, Business, and First Class. Each type offers different comfort levels and comes with corresponding pricing.");

                case "how can I customize the design of the plane":
                    return Json("You can customize the design of the plane by choosing from our available interior design options and selecting your preferred seating type. Additionally, you can explore design changes for the plane.");


                default:
                    return Json("I'm a simple bot. Ask me about aircraft!");
            }
        }
    }
}
