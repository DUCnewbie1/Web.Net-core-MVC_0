using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Areas.QuanTriVien.Controllers
{
    
    public class HomeAdminController : Controller
    {
        // GET: QuanTriVien/Home
        MyDataDataContext Data = new MyDataDataContext();
        public ActionResult Index()
        {
            return View();
        }
        // ------ĐĂNG NHẬP --------------
        public ActionResult DangNhap()
        {
            // Kiểm tra đã đăng nhập chưa
            if (Session["TaiKhoan"] != null || Session["TaiKhoanFB"] != null)
            {
                return RedirectToAction("Index", "HomeAdmin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(TaiKhoan user)
        {
            var taikhoanForm = user.TenDangNhap;
            var matkhauForm = MaHoaPassword.GetMd5Hash(user.MatKhauHash);

            // Kiểm tra thông tin đăng nhập
            var UserCheck = Data.TaiKhoans.SingleOrDefault(u => u.TenDangNhap.Equals(taikhoanForm) && u.MatKhauHash.Equals(matkhauForm));
            if (UserCheck != null)
            {
                // Kiểm tra xác nhận email
                var NguoiDung = Data.NguoiDungs.FirstOrDefault(t => t.MaNguoiDung == UserCheck.MaNguoiDung);
                if (NguoiDung != null && NguoiDung.IsEmailConfirmed == true)
                {
                    // Lưu thông tin tài khoản vào Session
                    Session["TaiKhoan"] = UserCheck;
                    Session["UserId"] = UserCheck.MaTaiKhoan;
                    Session["UserName"] = NguoiDung.Ten;
                    return RedirectToAction("Index", "HomeAdmin");
                }
                else
                {
                    // Đăng nhập thất bại vì email chưa được xác nhận
                    ViewBag.LoginFail = "Tài khoản chưa được xác nhận qua email.";
                    return View("DangNhap");
                }
            }
            else
            {
                ViewBag.LoginFail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("DangNhap");
            }
        }

        //tạo otp
        private string GenerateOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 1000000);
            return otp.ToString();
        }
        public ActionResult DangXuat()
        {
            // Xóa thông tin tài khoản khỏi Session
            Session["TaiKhoan"] = null;
            Session["UserId"] = null;
            Session["UserName"] = null;
            return RedirectToAction("DangNhap", "HomeAdmin");
        }
        //----------------------------------------------------------------------- ACTION QUAN LY NHAN VIEN
        public ActionResult IndexQLNV()
        {
            // lấy ra tất cả các thể loại của sách
            var all_nv = from s in Data.NhanViens select s;
            return View(all_nv);
        }
        //------------Detail-------------------------------
        public ActionResult DetailsQLNV(int id)
        {
            var D_nv = Data.NhanViens.Where(m => m.MaNhanVien == id).First();
            return View(D_nv);
        }
        //------------Create-------------------------------
        public ActionResult CreateQLNV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateQLNV(FormCollection collection, NhanVien s)
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
                Data.NhanViens.InsertOnSubmit(s); // Sửa thành InsertOnSubmit(s)
                Data.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
        //------------Edit-------------------------------//
        public ActionResult EditQLNV(int id)
        {
            var E_nv = Data.NhanViens.First(m => m.MaNhanVien == id);
            return View(E_nv);
        }
        [HttpPost]
        public ActionResult EditQLNV(int id, FormCollection collection)
        {
            var nv = Data.NhanViens.First(m => m.MaNhanVien == id);
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
                Data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.EditQLNV(id);
        }
        //------------Delete-------------------------------
        //-------------Delete-------------------
        public ActionResult DeleteQLNV(int id)
        {
            var D_nv = Data.NhanViens.First(m => m.MaNhanVien == id);
            return View(D_nv);
        }
        [HttpPost]
        public ActionResult DeleteQLNV(int id, FormCollection collection)
        {
            var D_nv = Data.NhanViens.Where(m => m.MaNhanVien == id).First();
            Data.NhanViens.DeleteOnSubmit(D_nv);
            Data.SubmitChanges();
            return RedirectToAction("Index");
        }
        //  kết thúc action quản lý nhân viên
        //--------------------------------------- ACTION QUẢN LÝ CÔNG TY----------------------------

        // Action Index show danh sach nha vien

        public ActionResult IndexCT()
        {
            var all_ct = from s in Data.CongTies select s;
            return View(all_ct);
        }
        //------------Detail-------------------------------
        public ActionResult DetailsCT(int id)
        {
            var D_ct = Data.CongTies.Where(m => m.MaCongTy == id).First();
            return View(D_ct);
        }
        //------------Create-------------------------------
        public ActionResult CreateQLCT()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateQLCT(FormCollection collection, CongTy s)
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
                Data.CongTies.InsertOnSubmit(s); // Sửa thành InsertOnSubmit(s)
                Data.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
        //------------Edit-------------------------------//
        public ActionResult EditCT(int id)
        {
            var E_ct = Data.CongTies.First(m => m.MaCongTy == id);
            return View(E_ct);
        }
        [HttpPost]
        public ActionResult EditCT(int id, FormCollection collection)
        {
            var ct = Data.CongTies.First(m => m.MaCongTy == id);
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
                Data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.EditCT(id);
        }
        //------------Delete-------------------------------
        //-------------Delete-------------------
        public ActionResult DeleteCT(int id)
        {
            var D_ct = Data.CongTies.First(m => m.MaCongTy == id);
            return View(D_ct);
        }
        [HttpPost]
        public ActionResult DeleteCT(int id, FormCollection collection)
        {
            var D_ct = Data.CongTies.Where(m => m.MaCongTy == id).First();
            Data.CongTies.DeleteOnSubmit(D_ct);
            Data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}