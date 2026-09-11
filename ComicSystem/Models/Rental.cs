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
        [Display(Name = "Mã đơn thuê")]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khách hàng.")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày thuê")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê"; // "Đang thuê", "Đã trả", "Có thể thuê"

        // Foreign Key Navigation
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        // Navigation property
        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
