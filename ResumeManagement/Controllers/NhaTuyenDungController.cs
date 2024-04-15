using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Controllers
{
    public class NhaTuyenDungController : Controller
    {
        // GET: NhaTuyenDung
        MyDataDataContext data = new MyDataDataContext();

        // Action Index show danh sach nha vien

        public ActionResult Index()
        {
            var all_ct = from s in data.CongTies select s;
            return View(all_ct);
        }
        //------------Detail-------------------------------
        public ActionResult Details(int id)
        {
            var D_ct = data.CongTies.Where(m => m.MaCongTy == id).First();
            return View(D_ct);
        }
        //------------Create-------------------------------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, CongTy s)
        {
            var tenct = collection["tenct"];
            if (string.IsNullOrEmpty(tenct))
            {
                ViewData["Error"] = "Don't empty";
                return View();
            }
            else
            {
                s.Ten = tenct;
                data.CongTies.InsertOnSubmit(s); // Sửa thành InsertOnSubmit(s)
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
        //------------Edit-------------------------------//
        public ActionResult Edit(int id)
        {
            var E_ct = data.CongTies.First(m => m.MaCongTy == id);
            return View(E_ct);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var ct = data.CongTies.First(m => m.MaCongTy == id);
            var E_tenct = collection["tenct"];
            ct.MaCongTy = id;
            if (string.IsNullOrEmpty(E_tenct))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                ct.Ten = E_tenct;
                UpdateModel(ct);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //------------Delete-------------------------------
        //-------------Delete-------------------
        public ActionResult Delete(int id)
        {
            var D_ct = data.CongTies.First(m => m.MaCongTy == id);
            return View(D_ct);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_ct = data.CongTies.Where(m => m.MaCongTy == id).First();
            data.CongTies.DeleteOnSubmit(D_ct);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}