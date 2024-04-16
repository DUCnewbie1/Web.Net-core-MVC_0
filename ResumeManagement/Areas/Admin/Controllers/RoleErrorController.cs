using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThatViet_Nhom3.Areas.Admin.Controllers
{
    public class RoleErrorController : Controller
    {
        // GET: Admin/ErrorRole
        public ActionResult KhongCoQuyen()
        {
            return View();
        }
    }
}