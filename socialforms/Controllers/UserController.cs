﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Controllers
{
    public class UserController
    {

    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Registration()
    {
        return View();
    }
}
