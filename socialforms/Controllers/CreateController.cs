using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Controllers {
    public class CreateController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
