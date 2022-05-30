using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using socialforms.Models;
using System.Data.Common;
using socialforms.Models.DB.sql;
using socialforms.Models.DB;

namespace socialforms.Controllers {
    public class CreateController : Controller {

        private IFormQuery _rep = new FormQuery();
        //private IQuestionQuery fQuestion = new QuestionQuery();
        public IActionResult Index()
        {
            try
            {
                if (!_rep.Connect())
                {
                    return View("_Message", new Message("Datenbankfehler", "Die Verbindung zur DB wurde nicht geöffnet", "Bitte versuchen Sie es später erneut!"));
                }
                else
                {
                    return View();
                }

            }
            catch (DbException)
            {
                return View("_Message", new Message("Datenbankfehler", "Es gab ein Problem mit der Datenbank!", "Bitte versuchen Sie es später erneut!"));
            }
            finally
            {
                _rep.Disconnect();
            }

        }

        [HttpPost]
        public IActionResult Index(Form qstData)
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
