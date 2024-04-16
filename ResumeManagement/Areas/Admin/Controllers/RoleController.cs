using Facebook;
using NoiThatViet_Nhom3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoiThatViet_Nhom3.App_Start;


namespace NoiThatViet_Nhom3.Areas.Admin.Controllers
{

    public class RoleController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();

        // GET: Admin/Role
        //id 1
        [AdminAuthorize(maChucNang = 1)]
        public ActionResult ThemMoi()
        {

            return View();
        }

        [HttpPost]
        [AdminAuthorize(maChucNang = 1)]
        public ActionResult ThemMoi(FormCollection collection, SanPham sp)
        {
            var ten = collection["TenSanPham"];
            var E_hinh = collection["HinhAnh"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {

                sp.HinhAnh = E_hinh.ToString();
                data.SanPhams.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("DanhSach");
            }
            return this.ThemMoi();
        }
        //id 2
        [AdminAuthorize(maChucNang = 2)]
        public ActionResult DanhSach()
        {
            var danhsachsp = data.SanPhams.ToList();
            return View(danhsachsp);
        }


        public ActionResult Xoa(string id)
        {

            var sp = data.SanPhams.First(m => m.MaSanPham == id);
            return View(sp);

        }
        [HttpPost]
        public ActionResult Xoa(string id, FormCollection collection)
        {
            var sp = data.SanPhams.Where(m => m.MaSanPham == id).First();
            data.SanPhams.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("DanhSach");
        }
        //id 4
        [AdminAuthorize(maChucNang = 3)]
        public ActionResult Sua(string id)
        {
            var sp = data.SanPhams.First(m => m.MaSanPham == id);

            return View(sp);
        }

        [HttpPost]
        [AdminAuthorize(maChucNang = 3)]
        public ActionResult Sua(string id, FormCollection collection)
        {
            var sp = data.SanPhams.First(m => m.MaSanPham == id);
            var tensp = collection["TenSanPham"];
            var E_hinh = collection["HinhAnh"];


            if (string.IsNullOrEmpty(tensp))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sp.HinhAnh = E_hinh.ToString();
                UpdateModel(sp);
                data.SubmitChanges();
                return RedirectToAction("DanhSach");
            }
            return this.Sua(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }




        //TAI KHOAN

        public ActionResult DanhSachTaiKhoan()
        {
            var dstaiKhoan = data.TaiKhoans.ToList();



            return View(dstaiKhoan);
        }

        public ActionResult ThemMoiUser()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ThemMoiUser(User user)
        {
            if (ModelState.IsValid)
            {

                // Tạo một đối tượng ThongTinCaNhan mới và lưu thông tin cá nhân
                var newThongTinCaNhan = new ThongTinCaNhan
                {
                    HoTen = user.Name,
                    Email = user.Email,
                    Sdt = int.Parse(user.SDT),
                    DiaChi = user.Diachi,
                    IsEmailConfirmed = true
                };

                data.ThongTinCaNhans.InsertOnSubmit(newThongTinCaNhan);
                data.SubmitChanges();

                // Lấy idTK của thông tin cá nhân mới được tạo
                int newIdTK = newThongTinCaNhan.idTK;

                // Tạo một đối tượng TaiKhoan mới và gán thông tin cần thiết
                var newUser = new TaiKhoan
                {
                    TenDangNhap = user.TaiKhoan,
                    MatKhau = MaHoaPassword.GetMd5Hash(user.Password), // Mã hóa password và lưu vào cơ sở dữ liệu
                    LoaiTK = "User", // Gán loại tài khoản
                    idTK = newIdTK // Gán idTK của thông tin cá nhân cho tài khoản
                };

                data.TaiKhoans.InsertOnSubmit(newUser);
                data.SubmitChanges();

                return RedirectToAction("DanhSachTaiKhoan");
            }

            return View(user); // Nếu model không hợp lệ, trả về view với model để hiển thị thông báo lỗi
        }

        public ActionResult SuaTaiKhoan(int id)
        {
            var taikhoan = data.TaiKhoans.First(m => m.idTK == id);
            return View(taikhoan);
        }


        [HttpPost]
        public ActionResult SuaTaiKhoan(int id, FormCollection collection)
        {
            var taikhoan = data.TaiKhoans.First(m => m.idTK == id);
            var DangNhap = collection["TenDangNhap"];
            var MatKhau = collection["MatKhau"];


            if (string.IsNullOrEmpty(DangNhap))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                taikhoan.MatKhau = MatKhau;
                UpdateModel(taikhoan);
                data.SubmitChanges();
                return RedirectToAction("DanhSachTaiKhoan");
            }
            return this.SuaTaiKhoan(id);
        }

        public ActionResult XoaTaiKhoan(int id)
        {

            var taikhoan = data.TaiKhoans.First(m => m.idTK == id);
            return View(taikhoan);

        }
        [HttpPost]
        public ActionResult XoaTaiKhoan(int id, FormCollection collection)
        {
            var tk = data.TaiKhoans.Where(m => m.idTK == id).First();
            data.TaiKhoans.DeleteOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("DanhSachTaiKhoan");
        }


    }
}