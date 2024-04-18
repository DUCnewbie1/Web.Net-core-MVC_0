using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeManagement.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        MyDataDataContext Data = new MyDataDataContext();
        public ActionResult Index()
        {
            return View();
        }
        // ------ĐĂNG NHẬP --------------
        public ActionResult DangNhap()
        {
            // Kiểm tra đã đăng nhập chưa
            if (Session["TaiKhoan"] != null)
            {
                // Lấy thông tin tài khoản từ session
                var taiKhoan = Session["TaiKhoan"] as TaiKhoan;

                // Kiểm tra loại tài khoản và chuyển hướng đến trang tương ứng
                if (taiKhoan != null)
                {
                    if (taiKhoan.MaCongTy != null)
                    {
                        // Chuyển hướng đến trang của công ty
                        return RedirectToAction("DanhSach", "BaiDang", new { area = "NhaTuyenDung" });
                    }
                    else if (taiKhoan.MaNguoiDung != null)
                    {
                        // Chuyển hướng đến trang của người dùng
                        return RedirectToAction("Index", "TinTuyenDung", new { area = "User" });
                    }
                    else if (taiKhoan.MaNhanVien != null)
                    {
                        // Chuyển hướng đến trang của nhân viên
                        return RedirectToAction("DanhSach", "DuyetBai", new { area = "NhanVien" });
                    }
                    else if (taiKhoan.MaQuanTriVien != null)
                    {
                        // Chuyển hướng đến trang của quản trị viên
                        return RedirectToAction("Index", "HomeAdmin", new { area = "QuanTriVien" });
                    }
                }
            }

            // Nếu không đăng nhập hoặc không có thông tin tài khoản, trả về trang đăng nhập mặc định
            return View("DangNhap");
        }


        [HttpPost]
        public ActionResult DangNhap(TaiKhoan user, string accountType)
        {
            var taikhoanForm = user.TenDangNhap;
            var matkhauForm = MaHoaPassword.GetMd5Hash(user.MatKhauHash);

            switch (accountType)
            {
                case "admin":
                    // Kiểm tra thông tin đăng nhập cho QUẢN TRỊ VIÊN
                    var adminCheck = Data.TaiKhoans.SingleOrDefault(u => u.TenDangNhap.Equals(taikhoanForm) && u.MatKhauHash.Equals(matkhauForm) && u.MaQuanTriVien != null);
                    if (adminCheck != null)
                    {
                        var quantrivien = Data.QuanTriViens.FirstOrDefault(t => t.MaQuanTriVien == adminCheck.MaQuanTriVien);
                        if (quantrivien != null)
                        {
                            // Lưu thông tin tài khoản vào Session
                            Session["TaiKhoan"] = adminCheck;
                            Session["UserId"] = adminCheck.MaTaiKhoan;
                            Session["UserName"] = quantrivien.Ten;
                            return RedirectToAction("Index", "HomeAdmin", new { area = "QuanTriVien" });
                        }
                    }
                    break;
                case "employer":
                    // Kiểm tra thông tin đăng nhập cho NHÀ TUYỂN DỤNG
                    var congTyCheck = Data.TaiKhoans.SingleOrDefault(u => u.TenDangNhap.Equals(taikhoanForm) && u.MatKhauHash.Equals(matkhauForm) && u.MaCongTy != null);
                    if (congTyCheck != null)
                    {
                        // Lưu thông tin tài khoản vào Session
                        Session["TaiKhoan"] = congTyCheck;
                        Session["UserId"] = congTyCheck.MaTaiKhoan;
                        Session["UserName"] = congTyCheck.MaCongTy;
                        return RedirectToAction("DanhSach", "BaiDang", new { area = "NhaTuyenDung" });
                    }
                    break;
                case "employee":
                    // Kiểm tra thông tin đăng nhập cho NHÂN VIÊN
                    var nhanVienCheck = Data.TaiKhoans.SingleOrDefault(u => u.TenDangNhap.Equals(taikhoanForm) && u.MatKhauHash.Equals(matkhauForm) && u.MaNhanVien != null);
                    if (nhanVienCheck != null)
                    {
                        // Lưu thông tin tài khoản vào Session
                        Session["TaiKhoan"] = nhanVienCheck;
                        Session["UserId"] = nhanVienCheck.MaTaiKhoan;
                        Session["UserName"] = nhanVienCheck.MaNhanVien;
                        return RedirectToAction("DanhSach", "DuyetBai", new { area = "nhanVien" });
                    }
                    break;
                case "user":
                    // Kiểm tra thông tin đăng nhập cho NGƯỜI DÙNG
                    var userCheck = Data.TaiKhoans.SingleOrDefault(u => u.TenDangNhap.Equals(taikhoanForm) && u.MatKhauHash.Equals(matkhauForm) && u.MaNguoiDung != null);
                    if (userCheck != null)
                    {
                        // Lưu thông tin tài khoản vào Session
                        Session["TaiKhoan"] = userCheck;
                        Session["UserId"] = userCheck.MaTaiKhoan;
                        Session["UserName"] = userCheck.MaNguoiDung;
                        return RedirectToAction("Index", "TinTuyenDung", new { area = "User" });
                    }
                    break;
            }

            // Trường hợp đăng nhập không thành công
            ViewBag.LoginFail = "Tài khoản mật khẩu không đúng hoặc không được cấp quyền";
            return View("DangNhap");
        }


        //tạo otp
        private string GenerateOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 1000000);
            return otp.ToString();
        }
    }
}