using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWebRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]

        [DisplayName("tên Category")]
        [MaxLength(30, ErrorMessage = "Tên danh mục không được vượt quá 30 ký tự")]
        public string Name { get; set; }

        [DisplayName("thứ tự hiển thị")]
        [Range(1, 100, ErrorMessage = "Thứ tự hiển thị phải từ 1 đến 100")]
        public int DisplayOrder { get; set; }
    }
}
