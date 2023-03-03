using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class SinhVien
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        public string MaSV { get; set; }
        [Required(ErrorMessage = "Tên sinh viên không được để trống")]
        public string TenSV { get; set; }
        public DateTime NgaySinh { get; set; }
        [RegularExpression("(Nam|Nữ)", ErrorMessage = "Giới tính không hợp lệ")]
        public string GioiTinh { get; set; }
        [Required(ErrorMessage = "Khoa không được để trống")]
        [RegularExpression(("Kinh tế|Ngôn ngữ Anh|Ngôn ngữ Hàn Quốc|Ngôn ngữ Nhật|Công nghệ thông tin|Truyền thông đa phương tiện"), 
            ErrorMessage = "Khoa phải là Kinh tế, Ngôn ngữ Anh, Ngôn ngữ Hàn Quốc, " +
            "Ngôn ngữ Nhật, Công nghệ thông tin hoặc Truyền thông đa phương tiện")]
        public string Khoa { get; set; }

    }
}
