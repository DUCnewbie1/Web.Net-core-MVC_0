using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Areas.NhaTuyenDung.Controllers
{
    public class BaiDangController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult DanhSach()
        {
            var danhsachbd = data.BaiDangTuyenDungs.ToList();
            return View(danhsachbd);
        }
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Them(FormCollection collection, BaiDangTuyenDung bd)
        {
            var tieude = collection["TieuDe"];

            if (string.IsNullOrEmpty(tieude))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                data.BaiDangTuyenDungs.InsertOnSubmit(bd);
                data.SubmitChanges();
                return RedirectToAction("DanhSach");
            }
            return this.Them();
        }
        public ActionResult CapNhat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var db = data.BaiDangTuyenDungs.FirstOrDefault(m => m.MaBaiDang == id);
            if (db == null)
            {
                return HttpNotFound();
            }

            return View(db);
        }

        [HttpPost]
        public ActionResult CapNhat(int? id, FormCollection collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var db = data.BaiDangTuyenDungs.FirstOrDefault(m => m.MaBaiDang == id);
            if (db == null)
            {
                return HttpNotFound();
            }

            var tieude = collection["TieuDe"];

            if (string.IsNullOrEmpty(tieude))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                UpdateModel(db);
                data.SubmitChanges();
                return RedirectToAction("DanhSach");
            }
            return RedirectToAction("CapNhat", new { id = id });
        }

        public ActionResult Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var db = data.BaiDangTuyenDungs.FirstOrDefault(m => m.MaBaiDang == id);
            if (db == null)
            {
                return HttpNotFound();
            }

            return View(db);
        }

        [HttpPost]
        public ActionResult Xoa(int? id, FormCollection collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var db = data.BaiDangTuyenDungs.FirstOrDefault(m => m.MaBaiDang == id);
            if (db == null)
            {
                return HttpNotFound();
            }

            data.BaiDangTuyenDungs.DeleteOnSubmit(db);
            data.SubmitChanges();
            return RedirectToAction("DanhSach");
        }

        public ActionResult DanhSachNguoiDung()
        {
            var dsNguoiDung = data.NguoiDungs.ToList();



            return View(dsNguoiDung);
        }

    }
}