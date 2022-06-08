using Microsoft.AspNetCore.Mvc;
using socialforms.Models;
using socialforms.Models.DB;
using socialforms.Models.DB.sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Controllers
{
    public class FormController:Controller
    {
        private IFormQuery _rep = new FormQuery();

        public IActionResult Delete(int id)
        {
            try
            {
                _rep.Connect();
                if (_rep.Delete(id))
                {
                    return View("_Message", new Message("Delete", "Form konnte nicht gelöscht werden"));
                }
                else
                {
                    return View("_Message", new Message("Delete", "Erfolgreich gelöscht"));
                }

            }
            catch (DbException)
            {
                return View("_Message", new Message("Delete", "Verbindung zur Datenbank fehlgeschlagen"));
            }
            finally
            {
                _rep.Disconnect();
            }
        }
    }
}
