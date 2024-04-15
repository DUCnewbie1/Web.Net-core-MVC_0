using ResumeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//-----------------------------------HIỆN TẠI CHƯA TẠO RA VIEW DANH SÁCH NGƯỜI DÙNG CHO NHÀ TUYỂN DỤNG ĐƯỢC(chỉ đang hiển thị tất cả người dùng)----------------------------- 
namespace ResumeManagement.Controllers
{
    public class ThongTinNguoiDungController : Controller
    {
        // GET: ThongTinNguoiDung
        
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_nd = from s in data.NguoiDungs select s;
            return View(all_nd);
        }
        //------------Detail-------------------------------
        public ActionResult Details(int id)
        {
            var D_nd = data.NguoiDungs.Where(m => m.MaNguoiDung == id).First();
            return View(D_nd);
        }
        //------------Block User----------------------------------

    }
}