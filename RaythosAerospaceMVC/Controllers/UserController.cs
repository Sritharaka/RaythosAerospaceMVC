using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;
using X.PagedList;

namespace RaythosAerospaceMVC.Controllers
{
    public class UserController : Controller
    {

        // Parameterless constructor
        //public UserController()
        //{
        //    // Any initialization logic, if needed
        //}

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User newUser)
        {
            //if (ModelState.IsValid)
            //{
                //    return View("Register", ModelState); // Pass ModelState to the view
                //}
                // Implement logic to validate and save user data to the database
                // Redirect to login page or homepage upon successful registration
                // Check if the email field is empty
                if (string.IsNullOrWhiteSpace(newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is required");
                    return View(newUser);
                }

                if (newUser == null)
                {
                    // Handle the case where newUser is null
                    // Example: return a BadRequest or handle the null case
                    return null;
                }

                // Ensure _userRepository is not null before using it
                if (_userRepository == null)
                {
                    // Handle the case where _userRepository is null
                    return Content("User repository is null");
                }

                //newUser.Id = 1; // Implement this method to generate a new Id
                //newUser.PasswordSalt = "123"; // Implement this method to generate a new Id

                bool existingUser = _userRepository.IsEmailAlreadyExists(newUser.Email);

                if (existingUser == true)
                {
                    // If the user email is already in use, return a view indicating that
                    ModelState.AddModelError("Register", "Email is already in used");
                    return View("Register", newUser); // Return the register view with the populated user object and error message
                }


                // Assuming you have password hashing logic and other validations here
                string result = _userRepository.AddUser(newUser);
                // Redirect to login page or homepage upon successful registration
                ////return RedirectToAction("Login", "Account", result); // Example: Redirect to Login action in Account controller
                ///
                if (result != null)
                {
                //    // User authenticated, perform further actions like setting authentication cookies or redirecting to a dashboard
                //    // For example:
                //    // FormsAuthentication.SetAuthCookie(user.Email, false);
                //    return RedirectToAction("RegisterSuccess"); // Replace "RegisterSuccess" with your desired action name
                var user = _userRepository.GetUser(newUser.Email);

                int userId = user.Id;
                HttpContext.Session.SetInt32("UserId", userId); // Store in session

                return RedirectToAction("Index", "Home"); // Redirect to Home/Index upon successful login

                }

                //return Content(result); // Example: Return a string response

            //}
            return View(newUser); // Return the view with validation errors if registration fails
        }


        public ActionResult Login()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", ModelState); // Pass ModelState to the view
            }

            // Authenticate user
            var user = _userRepository.AuthenticateUser(model.Email, model.Password);
            if (user != null)
            {
                // User authenticated, perform further actions like setting authentication cookies or redirecting to a dashboard
                // For example:
                // FormsAuthentication.SetAuthCookie(user.Email, false);
                // return RedirectToAction("Dashboard", "Home");

                int userId = user.Id;
                HttpContext.Session.SetInt32("UserId", userId); // Store in session

                return RedirectToAction("Index", "Home"); // Redirect to Home/Index upon successful login

            }

            ModelState.AddModelError("", "Invalid email or password");
            return View("Login", model);
        }

        public ActionResult RegisterSuccess()
        {
            return View(); // Return view after successful registration
        }

        //public ActionResult UserUpdate()
        //{
        //    return View("UserUpdate"); // Return view after successful registration
        //    //return RedirectToAction("UserUpdate", "User"); // Redirect to Home/Index upon successful login

        //}

        // GET: Aircrafts/Edit/5
        public async Task<IActionResult> UserUpdate()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

            var aircraft =  await _userRepository.UpdateUserByIdAsync(userId);
            if (aircraft == null)
            {
                return View("UserUpdate");
            }
            return View("UserUpdate", aircraft);
        }

        // POST: Aircrafts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserUpdate([Bind("Id, Username, Email, Password, " +
            "ConfirmPassword, PasswordSalt, CreateDate, UpdateDate, Address, FirstName, ImageUrl, LastName, PhoneNumber, Weight," +
            " CreatedDate")] User user, IFormFile uploadedImage)
        {
            //if (id != aircraft.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

                var existingUser = await _userRepository.GetUserByIdAsync(userId);
                if (existingUser == null)
                {
                    return RedirectToAction("index", "Home"); // Redirect to Home/Index upon successful login
                }

                try
                {


                    string imagePath = await _userRepository.UploadImage(uploadedImage);

                    // Set the ImageUrl property of the aircraft
                    user.ImageUrl = imagePath;
                    user.UpdateDate = DateTime.Now;
                    user.Id = userId;

                     var newUserData =  await _userRepository.UpdateUser(user);
                    //_logger.LogInformation($"Aircraft with ID {aircraft.Id} updated at {DateTime.Now}");

                    if (newUserData)
                    {
                        return RedirectToAction("index", "Home"); // Redirect to Home/Index upon successful login

                    }


                    //return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return BadRequest(); // Or handle the exception accordingly
                }
            }
            return View("UserUpdate");
        }


        public async Task<IActionResult> UserDetails()
        {

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                //return RedirectToAction("index", "Home"); // Redirect to Home/Index upon successful login
            }
            return PartialView("_UserDetailsPartial", existingUser);
        }

        public async Task<IActionResult> UserSideBar()
        {

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Retrieve from session

            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                //return RedirectToAction("index", "Home"); // Redirect to Home/Index upon successful login
            }
            return PartialView("_UserSideBarPartial", existingUser);
        }


        public async Task<IActionResult> _DisplayUserTable(int? page)
        {
            int pageNumber = page ?? 1; // If no page is specified, default to page 1
            int pageSize = 10; // Number of items per page

            var users = await _userRepository.GetUserList();

            // Return a paged list
            var pagedUsers = users.ToPagedList(pageNumber, pageSize);

            return PartialView("_DisplayUserTable", pagedUsers);
        }

        public ActionResult UserList()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var existingUser = await _userRepository.UpdateUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            return View(existingUser);
        }

        // POST: Aircrafts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, ModelName, Manufacturer, Description, " +
            "BasePrice, AirframeProgress, AvionicsSystemsProgress, EnginesProgress," +
            " InteriorProgress, OverallProgress, DeliveryDate, ManufacturingComplete, " +
            "Telephone, EmailAddress, CardHolderName, ShippingDate," +
            " AdditionalDetails, AirportCity, AircraftId, SeatingCapacity, Email, RoleId"
            )] User user)
        {
            //if (id != aircraft.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                try
                {



                    // Set the ImageUrl property of the aircraft
                    user.UpdateDate = DateTime.Now;

                    await _userRepository.UpdateAsync(user);
                    return RedirectToAction("UserList", "User"); // Redirect to Home/Index upon successful login

                    //return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return BadRequest(); // Or handle the exception accordingly
                }
            }
            return View(user);
        }

        // POST: Aircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("UserList", "User");
        }


    }
}
