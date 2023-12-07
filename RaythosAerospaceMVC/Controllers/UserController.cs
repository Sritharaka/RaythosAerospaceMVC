using Microsoft.AspNetCore.Mvc;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;
using System.Net;

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
                return RedirectToAction("Index", "Home"); // Redirect to Home/Index upon successful login

            }

            ModelState.AddModelError("", "Invalid email or password");
            return View("Login", model);
        }

        public ActionResult RegisterSuccess()
        {
            return View(); // Return view after successful registration
        }


    }
}
