using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Areas.User.Controllers
{
    public class TinTuyenDungController : Controller
    {
        // GET: TinTuyenDung
        MyDataDataContext data = new MyDataDataContext();

        // Action Index show danh sach nha vien

        public ActionResult Index()
        {
            var all_td = from s in data.BaiDangTuyenDungs select s;
            return View(all_td);
        }
        //------------Detail-------------------------------
        public ActionResult Details(int id)
        {
            var D_td = data.BaiDangTuyenDungs.Where(m => m.MaBaiDang == id).First();
            return View(D_td);
        }
        //------------Create-------------------------------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, BaiDangTuyenDung s)
        {
            var tentd = collection["tentd"];
            if (string.IsNullOrEmpty(tentd))
            {
                ViewData["Error"] = "Don't empty";
                return View();
            }
            else
            {
                s.TieuDe = tentd;
                data.BaiDangTuyenDungs.InsertOnSubmit(s); // Sửa thành InsertOnSubmit(s)
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
        //------------Edit-------------------------------//
        public ActionResult Edit(int id)
        {
            var E_ct = data.BaiDangTuyenDungs.First(m => m.MaBaiDang == id);
            return View(E_ct);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var td = data.BaiDangTuyenDungs.First(m => m.MaBaiDang == id);
            var E_tentd = collection["tentd"];
            td.MaBaiDang = id;
            if (string.IsNullOrEmpty(E_tentd))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                td.TieuDe = E_tentd;
                UpdateModel(td);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //------------Delete-------------------------------
        public ActionResult Delete(int id)
        {
            var D_td = data.BaiDangTuyenDungs.First(m => m.MaBaiDang == id);
            return View(D_td);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_td = data.BaiDangTuyenDungs.Where(m => m.MaBaiDang == id).First();
            data.BaiDangTuyenDungs.DeleteOnSubmit(D_td);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        //-----------------Đăng xuất người dùng----------------------------------------
        public ActionResult DangXuat()
        {
            // Xử lý đăng xuất ở đây

            // Redirect đến trang đăng nhập
            return Redirect("~/Areas/QuanTriVien/Views/HomeAdmin/DangNhap.cshtml");
        }
    }
}