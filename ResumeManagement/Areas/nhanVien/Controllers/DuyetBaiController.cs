using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Areas.nhanVien.Controllers
{
    public class DuyetBaiController : Controller
    {
        // GET: DuyetBai/nhanVien
        MyDataDataContext data = new MyDataDataContext();

        public ActionResult DanhSach()
        {
            var danhsachbd = data.BaiDangTuyenDungs.ToList();
            return View(danhsachbd);
        }

        public ActionResult DuyetBai(int? id)
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

            int? maNhanVien = (from bd in data.BaiDangTuyenDungs
                               where bd.MaNhanVien != null
                               select bd.NhanVien.MaNhanVien).FirstOrDefault();

            // Kiểm tra nếu mã nhân viên đã tồn tại
            if (maNhanVien != null)
            {
                // Cập nhật mã nhân viên cho bài đăng
                db.MaNhanVien = maNhanVien.Value; // Lấy giá trị int từ int?
                data.SubmitChanges();

                return RedirectToAction("DanhSach");
            }
            else
            {
                // Nếu không tìm thấy mã nhân viên, xử lý theo nhu cầu của bạn
                // Ví dụ: hiển thị thông báo lỗi, chuyển hướng đến trang khác, v.v.
                return View("Error");
            }
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

        public ActionResult DanhSachCongTy()
        {
            var danhsachCT = data.CongTies.ToList();
            return View(danhsachCT);
        }

    }
}