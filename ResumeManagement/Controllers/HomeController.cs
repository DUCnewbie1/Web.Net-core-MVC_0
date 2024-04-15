using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Net;

namespace ResumeManagement.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext Data = new MyDataDataContext();

        public ActionResult DangNhap()
        {
            // Kiểm tra đã đăng nhập chưa
            if (Session["TaiKhoan"] != null || Session["TaiKhoanFB"] != null)
            {
                return RedirectToAction("Index", "TinTuyenDung");
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
                    return RedirectToAction("Index", "TinTuyenDung");
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
            return RedirectToAction("DangNhap", "Home");
        }
    }
}