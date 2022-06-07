using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using socialforms.Models;
using System.Data.Common;
using socialforms.Models.DB.sql;
using socialforms.Models.DB;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;


namespace socialforms.Controllers {
    public class CreateController : Controller
    {
        const String curUserId = "-1";
        private IFormQuery _rep = new FormQuery();
        private IQuestionQuery _qstrep = new QuestionQuery();
        public IActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetInt32(curUserId) == null) {
                    return RedirectToAction("Login", "User");
                }
                Debug.WriteLine(HttpContext.Session.GetInt32(curUserId));
                if (!_rep.Connect())
                {
                    return View("_Message", new Message("Datenbankfehler", "Die Verbindung zur DB wurde nicht geöffnet", "Bitte versuchen Sie es später erneut!"));
                }
                //else if()
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

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(FormWithQuestions qstData)
        {
            if(qstData == null)
            {
                return RedirectToAction("Insert");
            }

            ValidateData(qstData);

            if (ModelState.IsValid)
            {
                try
                {
                    _rep.Connect();
                    _qstrep.Connect();
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32(curUserId));
                    qstData.SForm.UserId = Convert.ToInt32(HttpContext.Session.GetInt32(curUserId));
                    qstData.SForm.CreateDate = DateTime.Now;
                    if (_rep.Insert(qstData.SForm))
                    {
                        int fId = _rep.findByName(qstData.SForm.FormName, userId).FormId;
                        foreach (String s in qstData.QstList)
                        { 
                            Question qst = new Question();
                            qst.Qtext = s;
                            qst.FormId = fId;
                            qst.QuestionType = 1;
                            _qstrep.Insert(qst);
                        }
                        return View("_Message", new Message("Erstellen", "Ihre Daten wurden erfolgreich abgespeichert"));
                    }
                    else
                    {
                        return View("_Message", new Message("Erstellen", "Bitte versuchen Sie es später erneut"));
                    }

                }
                catch (DbException)
                {
                    return View("_Message", new Message("Erstellen", "Datenbankfehler", "Bitte versuchen Sie es später erneut"));
                }
                finally
                {
                    _rep.Disconnect();
                    _qstrep.Disconnect();
                }


            }

            return View(qstData);
        }

        private void ValidateData(FormWithQuestions f)
        {
            if (f == null)
            {
                return;
            }
        }
    }


      
   

}

    
