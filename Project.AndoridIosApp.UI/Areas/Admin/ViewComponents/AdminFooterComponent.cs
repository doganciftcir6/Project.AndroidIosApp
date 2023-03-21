﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AdminFooterComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
