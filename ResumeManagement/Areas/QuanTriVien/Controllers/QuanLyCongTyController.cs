using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Controllers
{
    public class QuanLyCongTyController : Controller
    {
        // GET: QuanTriVien/QuanLyCongTy
        MyDataDataContext data = new MyDataDataContext();

        // Action Index show danh sach nha vien

        public ActionResult Index()
        {
            
            return View();
        }
       
    }
}