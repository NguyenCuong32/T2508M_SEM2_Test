using System;
using System.Collections.Generic;

namespace T2508M_SEM_Test.Models;

public partial class Rental
{
    public int RentalId { get; set; }

    public int CustomerId { get; set; }

    public DateTime RentalDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Rentaldetail> Rentaldetails { get; set; } = new List<Rentaldetail>();
}
