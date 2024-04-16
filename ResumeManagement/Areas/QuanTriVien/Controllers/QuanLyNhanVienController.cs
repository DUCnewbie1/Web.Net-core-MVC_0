using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Controllers
{
    public class QuanLyNhanVienController : Controller
    {
        // GET: QuanTriVien/QuanLyNhanVien
        public class QuanLyCongTyController : Controller
        {
            // GET: QuanTriVien/QuanLyCongTy
            MyDataDataContext data = new MyDataDataContext();

            // Action Index show danh sach nha vien

            public ActionResult IndexQLNV()
            {
                // lấy ra tất cả các thể loại của sách
                var all_nv = from s in data.NhanViens select s;
                return View(all_nv);
            }
            //------------Detail-------------------------------
            public ActionResult Details(int id)
            {
                var D_nv = data.NhanViens.Where(m => m.MaNhanVien == id).First();
                return View(D_nv);
            }
            //------------Create-------------------------------
            public ActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Create(FormCollection collection, NhanVien s)
            {
                var tennv = collection["tennv"];
                if (string.IsNullOrEmpty(tennv))
                {
                    ViewData["Error"] = "Don't empty";
                    return View();
                }
                else
                {
                    s.Ten = tennv;
                    data.NhanViens.InsertOnSubmit(s); // Sửa thành InsertOnSubmit(s)
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
            }
            //------------Edit-------------------------------//
            public ActionResult Edit(int id)
            {
                var E_nv = data.NhanViens.First(m => m.MaNhanVien == id);
                return View(E_nv);
            }
            [HttpPost]
            public ActionResult Edit(int id, FormCollection collection)
            {
                var nv = data.NhanViens.First(m => m.MaNhanVien == id);
                var E_tennv = collection["tennv"];
                nv.MaNhanVien = id;
                if (string.IsNullOrEmpty(E_tennv))
                {
                    ViewData["Error"] = "Don't empty!";
                }
                else
                {
                    nv.Ten = E_tennv;
                    UpdateModel(nv);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return this.Edit(id);
            }
            //------------Delete-------------------------------
            //-------------Delete-------------------
            public ActionResult Delete(int id)
            {
                var D_nv = data.NhanViens.First(m => m.MaNhanVien == id);
                return View(D_nv);
            }
            [HttpPost]
            public ActionResult Delete(int id, FormCollection collection)
            {
                var D_nv = data.NhanViens.Where(m => m.MaNhanVien == id).First();
                data.NhanViens.DeleteOnSubmit(D_nv);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
    }
}