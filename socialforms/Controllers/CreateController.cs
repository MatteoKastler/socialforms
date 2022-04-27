using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using socialforms.Models;

namespace socialforms.Controllers {
    public class CreateController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Createform()
        {
            return View();
        }

    }

}
