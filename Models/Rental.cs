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
        public int RentalID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Display(Name = "Ngày thuê")]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Display(Name = "Ngày trả")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);

        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê";

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
