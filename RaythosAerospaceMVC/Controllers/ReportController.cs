using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repositories;
using RaythosAerospaceMVC.Repository;
using Rotativa.AspNetCore.Options;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace RaythosAerospaceMVC.Controllers
{
    public class ReportController : Controller
    {

        private readonly AerospaceDbContext _context; // Replace YourDbContext with your EF Core DbContext


        private readonly IPaymentRepository _paymentRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IManufacturingProgressRepository _manufacturingProgressRepository;
        private readonly IUserRepository _userRepository;

        public ReportController(IPaymentRepository paymentRepository, IInventoryRepository inventoryRepository, IAircraftRepository aircraftRepository, IUserRepository userRepository, IManufacturingProgressRepository manufacturingProgressRepository)
        {
            _paymentRepository = paymentRepository;
            _inventoryRepository = inventoryRepository;
            _aircraftRepository = aircraftRepository;
            _userRepository = userRepository;
            _manufacturingProgressRepository = manufacturingProgressRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Report()
        {
            var viewModel = new ReportViewModel
            {
                Payments = await _paymentRepository.GetAllPayments(),
                Inventory = await _inventoryRepository.GetAllInventoriesAsync(),
                Aircrafts = await _aircraftRepository.GetAllAircraftsAsync(),
                Users = await _userRepository.GetUserList(),
                ManufacturingProgresses = await _manufacturingProgressRepository.GetAllAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> TablePartial(string tableName)
        {
            switch (tableName)
            {
                case "Payments":
                    var payments = await _paymentRepository.GetAllPayments();
                    return PartialView("_Payments", payments);

                case "Inventory":
                    var inventories = await _inventoryRepository.GetAllInventoriesAsync();
                    return PartialView("_Inventory", inventories);

                case "Aircrafts":
                    var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();
                    return PartialView("_Aircrafts", aircrafts);

                case "Users":
                    var users = await _userRepository.GetUserList();
                    return PartialView("_Users", users);

                case "ManufacturingProgresses":
                    var manufacturingProgresses = await _manufacturingProgressRepository.GetAllAsync();
                    return PartialView("_ManufacturingProgresses", manufacturingProgresses);

                default:
                    var defaultPayments = await _paymentRepository.GetAllPayments();
                    return PartialView("_Payments", defaultPayments); // Default table
            }
        }

        public async Task<IActionResult> GenerateReport(string tableSelect, DateTime? fromDate, DateTime? toDate)
        {
            switch (tableSelect)
            {
                case "Payments":
                    var payments = await _paymentRepository.GetPaymentsByDateRange(fromDate, toDate);
                    return PartialView("_Payments", payments);

                case "Inventory":
                    var inventories = await _inventoryRepository.GetInventoriesByDateRange(fromDate, toDate);
                    return PartialView("_Inventory", inventories);

                case "Aircrafts":
                    var aircrafts = await _aircraftRepository.GetAircraftsByDateRange(fromDate, toDate);
                    return PartialView("_Aircrafts", aircrafts);

                case "Users":
                    var users = await _userRepository.GetUsersByDateRange(fromDate, toDate);
                    return PartialView("_Users", users);

                case "ManufacturingProgresses":
                    var manufacturingProgresses = await _manufacturingProgressRepository.GetProgressesByDateRange(fromDate, toDate);
                    return PartialView("_ManufacturingProgresses", manufacturingProgresses);

                default:
                    var defaultPayments = await _paymentRepository.GetPaymentsByDateRange(fromDate, toDate);
                    return PartialView("_Payments", defaultPayments); // Default table
            }
        }



        //public async Task<IActionResult> Report()
        //{
        //    var payments = await _paymentRepository.GetAllPayments();
        //    var inventories = await _inventoryRepository.GetAllInventoriesAsync();
        //    var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();
        //    var users = await _userRepository.GetUserList();
        //    var manufacturingProgresses = await _manufacturingProgressRepository.GetAllAsync();

        //    // Generate report content based on retrieved data
        //    var reportContent = "<h1>Report Content</h1>";
        //    reportContent += "<h2>Payments</h2><ul>";
        //    foreach (var payment in payments)
        //    {
        //        reportContent += $"<li>Payment ID: {payment.Id}, Amount: {payment.BasePrice}</li>";
        //        // Include other payment properties as needed
        //    }
        //    reportContent += "</ul>";

        //    // Include content for other entities (Inventories, Aircrafts, Users, ManufacturingProgresses) similarly...

        //    var pdfContent = await GeneratePdf(reportContent);

        //    return File(pdfContent, "application/pdf", "report.pdf");
        //}

        //private async Task<byte[]> GeneratePdf(string htmlContent)
        //{
        //    var pdf = new Rotativa.AspNetCore.ViewAsPdf("Report", htmlContent)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
        //        // Add other Rotativa options as needed
        //    };

        //    return await pdf.BuildFile(ControllerContext);
        //}

        //public async Task<IActionResult> Report()
        //{
        //    var payments = await _paymentRepository.GetAllPayments();
        //    var inventories = await _inventoryRepository.GetAllInventoriesAsync();
        //    var aircrafts = await _aircraftRepository.GetAllAircraftsAsync();
        //    var users = await _userRepository.GetUserList();
        //    var manufacturingProgresses = await _manufacturingProgressRepository.GetAllAsync();

        //    // Generate report content based on retrieved data
        //    var reportContent = "<h1>Report Content</h1>";
        //    reportContent += "<h2>Payments</h2><ul>";
        //    foreach (var payment in payments)
        //    {
        //        reportContent += $"<li>Payment ID: {payment.Id}, Amount: {payment.BasePrice}</li>";
        //        // Include other payment properties as needed
        //    }
        //    reportContent += "</ul>";

        //    // Include content for other entities (Inventories, Aircrafts, Users, ManufacturingProgresses) similarly...

        //    var pdfContent = await GeneratePdf(reportContent);

        //    return File(pdfContent, "application/pdf", "report.pdf");
        //}

        //private async Task<byte[]> GeneratePdf(string htmlContent)
        //{
        //    var globalSettings = new GlobalSettings
        //    {
        //        ColorMode = DinkToPdf.ColorMode.Color,
        //        Orientation = DinkToPdf.Orientation.Portrait,
        //        PaperSize = DinkToPdf.PaperKind.A4
        //    };

        //    var objectSettings = new ObjectSettings
        //    {
        //        HtmlContent = htmlContent,
        //        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "styles.css") }
        //    };

        //    var pdf = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = globalSettings,
        //        Objects = { objectSettings }
        //    };

        //    var converter = new SynchronizedConverter(new PdfTools());
        //    return converter.Convert(pdf);
        //}





    }
}
