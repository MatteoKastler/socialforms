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
        private IQuestionQuery fQuestion = new QuestionQuery();
        public IActionResult Index()
        {
            try
            {
                _rep.Connect();
                Form form = _rep.getForm(1);
                if (form == null)
                {
                    return View("_Message", new Message("Datenbankfehler", "Die Verbindung zur DB wurde nicht geöffnet", "Bitte versuchen Sie es später erneut!"));
                }
                else
                {
                    return View(form);
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

        private void ValidateRegistrationData(Question q)
        {
            if (q == null)
            {
                return;
            }

            if ((q.Qtext == null))
            {
                ModelState.AddModelError("Question", "Bitte eine Frage eingeben");
            }
        }

    }

    


 }
