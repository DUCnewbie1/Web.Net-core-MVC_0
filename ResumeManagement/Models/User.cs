using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ResumeManagement.Models
{
    public class User
    {
        [Key]
        public int UserPId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TenDangNhap")]
        public string TenTaiKhoan { get; set; }

        [Required]
        [MaxLength(64)]
        [Column("MatKhauHash")]
        public byte[] MatKhauHash { get; set; }

        [NotMapped] // Không được ánh xạ vào cơ sở dữ liệu
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(20)]
        [Column("LoaiTaiKhoan")]
        public string AccountType { get; set; }

        [ForeignKey("MaQuanTriVien")]
        public int? AdminId { get; set; }

        [ForeignKey("MaNguoiDung")]
        public int? UserId { get; set; }

        [ForeignKey("MaCongTy")]
        public int? CompanyId { get; set; }

        [ForeignKey("MaNhanVien")]
        public int? EmployeeId { get; set; }

        // Navigation properties
        public virtual QuanTriVien Admin { get; set; }
        public virtual NguoiDung UserPfId { get; set; }
        public virtual CongTy Company { get; set; }
        public virtual NhanVien Employee { get; set; }

        // Additional methods or properties can be added as needed
    }
}
