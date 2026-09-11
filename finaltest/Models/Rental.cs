using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finaltest.Models
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
        [Display(Name = "Ngày thuê (Rental date)")]
        public DateTime RentalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả (Return date)")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(7);

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Đang thuê";

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
