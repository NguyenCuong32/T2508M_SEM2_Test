using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Rentals")]
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày thuê")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày thuê (RentalDate)")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả (ReturnDate)")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái (Status)")]
        public string Status { get; set; } = "Đang thuê";

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
