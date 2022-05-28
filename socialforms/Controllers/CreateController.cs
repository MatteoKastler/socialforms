using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using socialforms.Models;
using System.Data.Common;
using socialforms.Models.DB.sql;

namespace socialforms.Controllers {
    public class CreateController : Controller {

        private FormQuery _rep = new FormQuery();
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Createform()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Createform(Form qstData)
        {
            if (qstData == null)
            {
                return RedirectToAction("Createform");
            }

            ValidateRegistrationData(qstData);


            if (ModelState.IsValid)
            {
                try
                {
                    _rep.Connect();
                    if (_rep.Insert(qstData))
                    {
                        return View("_Message", new Message("Creation", "Ihr socialform wurde erfolgreich abgespeichert"));
                    }
                    else
                    {
                        return View("_Message", new Message("Creation", "Bitte versuchen Sie es später erneut"));
                    }

                }
                catch (DbException)
                {
                    return View("_Message", new Message("Creation", "Datenbankfehler", "Bitte versuchen Sie es später erneut"));
                }
                finally
                {
                    _rep.Disconnect();
                }


            }

            return View(qstData);
        }

        private void ValidateRegistrationData(Form f)
        {
            if (f == null)
            {
                return;
            }
        }

    }

    


 }
