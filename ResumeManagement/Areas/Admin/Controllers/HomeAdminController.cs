using Microsoft.Ajax.Utilities;
using NoiThatViet_Nhom3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NoiThatViet_Nhom3.Areas.Admin.Controllers
{
	public class HomeAdminController : Controller
	{
		MyDataDataContext data = new MyDataDataContext();
		// GET: Admin/HomeAdmin
		public ActionResult Index()
		{
			if (Session["User"] == null)
			{
				return RedirectToAction("DangNhap");
			}
			return View();
		}
		public ActionResult DangNhap()
		{
			
			return View();
		}

		[HttpPost]
		public ActionResult DangNhap(string taiKhoan , string matKhau)
		{
			var uservar = data.TaiKhoans.SingleOrDefault(s => s.TenDangNhap.ToLower() == taiKhoan.ToLower() && s.MatKhau == matKhau); 
			if(uservar != null)
			{
				if (uservar.LoaiTK.Trim().ToLower() == "user")
				{
					TempData["error"] = "Người dùng không được phép đăng nhập.";
					return View();
				}


				Session["User"] = uservar;
				
				return RedirectToAction("Index");
			}
			else
			{
				@TempData["error"] = "Tài Khoản không đúng";
				return View();
			}
		}


		//Đăng xuất
		public ActionResult DangXuat()
		{
			Session["User"] = null;
			FormsAuthentication.SignOut();
			return RedirectToAction("DangNhap");

		}
	}
}