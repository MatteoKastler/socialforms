using Microsoft.AspNetCore.Mvc;
using socialforms.Models;
using socialforms.Models.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace socialforms.Controllers {
    public class UserController : Controller {

        private IUsersquery _rep = new Userquery();
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registration() {
            return View();
        }
        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Registration(User userDataFromForm) {
            if (userDataFromForm == null) {
                return RedirectToAction("Registration");
            }

            ValidateRegistrationData(userDataFromForm);

            if (ModelState.IsValid) {


                return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgespeichert"));

            }
            return View(userDataFromForm);
        }

        [HttpPost]
        public IActionResult Login(User userDataFromForm)
        {
            if (userDataFromForm == null)
            {
                return RedirectToAction("Login");
            }

            ValidateRegistrationData(userDataFromForm);

            if (ModelState.IsValid)
            {


                return View("_Message", new Message("Login", "Sie haben sich erfolgreich eingelogt."));

            }
            return View(userDataFromForm);
        }

        [HttpPost]
        public IActionResult Settings(User userDataFromForm)
        {
            if (userDataFromForm == null)
            {
                return RedirectToAction("Settings");
            }
            return View(userDataFromForm);
        }




        private void ValidateRegistrationData(User u)
        {
            if (u == null)
            {
                return;
            }

            //Username
            if ((u.Username == null) || (u.Username.Trim().Length < 4))
            {
                ModelState.AddModelError("Username", "Der Benutzername muss mind. 4 Zeichen lang sein!");
            }

            //Passwort
            if ((u.Password != null) || (u.Password.Length < 8))
            {
                ModelState.AddModelError("Password", "Das Password muss mindestens 8 Zeichen lang sein");
            }

            //EMail
            string pattern = @"\A(?:[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            if (Regex.IsMatch(pattern, u.Email))
            {
                ModelState.AddModelError("Password", "Bitte geben sie eine gültige EMail-Adresse im Format xyz@abc.de ein!");
            }

            //Birthdate
            /*if (DateTime.TryParseExact(str, "MM/dd/yyyy", null, DateTimeStyles.None, u.Birthdate) == true)
            {
                ModelState.AddModelError("Birthdate", "Bitte ein gültiges Datum im Format MM/dd/yyyy eingeben");
            }*/
        }
    }
   
}

