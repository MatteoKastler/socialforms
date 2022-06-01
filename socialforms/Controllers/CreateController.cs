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
    public class CreateController : Controller
    {

        private IFormQuery _rep = new FormQuery();
        private IQuestionQuery _qstrep = new QuestionQuery();
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
        public IActionResult Insert(FormWithQuestions qstData)
        {
            if(qstData == null)
            {
                return RedirectToAction("InsertQst");
            }

            ValidateRegistrationData(qstData);

            if (ModelState.IsValid)
            {
                try
                {
                    _rep.Connect();
                    _qstrep.Connect();
                    if (_rep.Insert(qstData.FormName))
                    {
                        foreach(Question q in qstData.Questions)
                        {
                            _qstrep.Insert(q);
                        }
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
                    _rep.Disconnect();
                    _qstrep.Disconnect();
                }


            }

            return View(qstData);
        }

        private void ValidateRegistrationData(FormWithQuestions f)
        {
            if (f == null)
            {
                return;
            }
        }
    }


      
   

}

    
