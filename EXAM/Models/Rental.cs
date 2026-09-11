using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXAM.Models
{
    [Table("Rentals")]
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã đơn thuê")]
        public int RentalID { get; set; }

        [Required]
        [Display(Name = "Khách hàng")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Ngày thuê")]
        [DataType(DataType.DateTime)]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Display(Name = "Ngày trả")]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string? Status { get; set; }

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}

