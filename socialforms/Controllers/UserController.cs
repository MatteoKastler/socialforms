using Microsoft.AspNetCore.Mvc;
using socialforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Controllers {
    public class UserController : Controller {
        public IActionResult index() {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(User userDataFromForm)
        {
            //Parameter überprüfen
            if (userDataFromForm == null)
            {
                return RedirectToAction("Registration");
            }

            //Formulardaten überprüfen (Validierung - serverseitig)
            ValidateRegistrationData(userDataFromForm);

            //alle Daten des Formulars sind richtig
            if (ModelState.IsValid)
            {
                //in DB-Tabelle abspeichern

                return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgespeichert"));
                //Falls die Validierung nicht erfolgreich war, wird das Formular mit dem eingeg. daten befüllt und wieder angezeigt
            }

            //TODO: in DB-Tabelle abspeichern


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
            //min ein Kleinbuchstabe, ein Großbuchstabe, ein Sonderzeichen und eine Zahl

            //Birthdate

            //EMail
        }
    }
   
}

