using Microsoft.AspNetCore.Mvc;
using socialforms.Models;
using socialforms.Models.DB;
using socialforms.Models.DB.sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace socialforms.Controllers {
    public class UserController : Controller {

        private IUsersquery _rep = new Userquery();
        private IFormQuery _form = new FormQuery();
        public IActionResult Index()
        {
            try {
                _rep.Connect();
                _form.Connect();
                User user = _rep.GetUser(Convert.ToInt32(HttpContext.Session.GetInt32(curUserId)));
                if (user == null) {
                    return RedirectToAction("Login");
                } else {
                    Debug.WriteLine(user.Username);
                    ViewBag.forms = _form.getForms(user.PersonId); // --> funzt alles super, da kommen forms raus und alles
                    foreach(Form f in ViewBag.forms) {
                        f.answers = _form.cntUseranswers(f.FormId);
                        f.questions = _form.cntQuestions(f.FormId);
                    }
                    return View(user);
                }
            }catch (DbException e) {
                Debug.WriteLine(e.Message);
                return View("_Message", new Message("Datenbankfehler", "Es gab ein Problem mit der Datenbank!", "Bitte versuchen Sie es später erneut!"));
            } finally {
                _rep.Disconnect();
                _form.Disconnect();
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
                    _rep.Connect();
                    if (_rep.Insert(userDataFromForm))
                    {
                        return RedirectToAction("Index");
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
                   // _rep.Disconnect();
                }


            }

            return View(userDataFromForm);
        }

        const string curUserId = "-1";
        [HttpPost]
        public IActionResult Login(User userDataFromForm)
        {
            _rep.Connect();
            if (userDataFromForm == null)
            {
                return View();
            }
            Debug.WriteLine(userDataFromForm.Password);

            User u = _rep.Login(userDataFromForm.Username, userDataFromForm.Password);
            if (u != null)
            {
                HttpContext.Session.SetInt32(curUserId, u.PersonId);
                return RedirectToAction("Index");

            } else {
                return View("_Message", new Message("Login", "Daten konnten nicht geprüft werde"));
            }
            
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

            if (!IsValidEmail(u.Email)) {
                ModelState.AddModelError("Password", "Bitte geben Sie eine gültige EMail-Adresse im Format xyz@abc.de ein!");
            }

        }
        bool IsValidEmail(string email) {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith(".")) {
                return false;
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

