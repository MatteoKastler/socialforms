using Microsoft.AspNetCore.Mvc;
using socialforms.Models;
using socialforms.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace socialforms.Controllers {
    public class UserController : Controller {

        private IUsersquery uQuery = new Userquery();
        public IActionResult Index()
        {
            try
            {
                uQuery.Connect();
                User user = uQuery.GetUser(1); //get user from session
                if (user == null)
                {
                    return View("_Message", new Message("Datenbankfehler", "Die Verbindung zur DB wurde nicht geöffnet", "Bitte versuchen Sie es später erneut!"));
                }
                else
                {
                    Debug.WriteLine(user.Username);
                    return View(user); //WICHTIG: in der Index view vom user braucht die tabelle außer dem user no a anderes model
                }
            }
            catch (DbException e)
            {
                Debug.WriteLine(e.Message);
                return View("_Message", new Message("Datenbankfehler", "Es gab ein Problem mit der Datenbank!", "Bitte versuchen Sie es später erneut!"));
            }
            finally
            {
                uQuery.Disconnect();
            }


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
        public IActionResult createform()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Registration(User userDataFromForm)
        {

            if (userDataFromForm == null)
            {
                return RedirectToAction("Registration");
            }

            ValidateRegistrationData(userDataFromForm);


            if (ModelState.IsValid)
            {
                try
                {
                    uQuery.Connect();
                    if (uQuery.Insert(userDataFromForm))
                    {
                        return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgespeichert"));
                    }
                    else
                    {
                        return View("_Message", new Message("Registrierung", "Bitte versuchen Sie es später erneut"));
                    }

                }
                catch (DbException)
                {
                    return View("_Message", new Message("Registrierung", "Datenbankfehler", "Bitte versuchen Sie es später erneut"));
                }
                finally
                {
                    uQuery.Disconnect();
                }


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
            try {
                uQuery.Connect();
                User temp = uQuery.Login(userDataFromForm.Username, userDataFromForm.Password);
                Debug.WriteLine(userDataFromForm.Username + " " + userDataFromForm.Password);

                if (temp == null) {
                    return View("_Message", new Message("Login", "LoginFehler", "Kein user zu den Daten gefunden oder Passwort falsch"));//user in session speichern?
                } else {
                    return View("Index", temp);
                }
            }catch(DbException e) {

            } finally {
                uQuery.Disconnect();
            }
            return null;
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
            if ((u.Password == null) || (u.Password.Length < 8))
            {
                ModelState.AddModelError("Password", "Das Password muss mindestens 8 Zeichen lang sein");
            }

            //EMail
            Debug.WriteLine(u.Email);
            if (!IsValidEmail(u.Email))
            {
                ModelState.AddModelError("Password", "Bitte geben Sie eine gültige EMail-Adresse im Format xyz@abc.de ein!");
            }

        }
        bool IsValidEmail(string email) {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith(".")) {
                return false; // suggested by @TK-421
            }
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            } catch {
                return false;
            }
        }
    }
   
}

