using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    [Table("Rentals")]
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã phiếu thuê")]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày thuê")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày thuê (RentalDate)")]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Vui lòng nhập ngày trả")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày trả (ReturnDate)")]
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);

        [Required(ErrorMessage = "Vui lòng nhập trạng thái")]
        [StringLength(50)]
        [Display(Name = "Trạng thái (Status)")]
        public string Status { get; set; } = "Đang thuê";

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
