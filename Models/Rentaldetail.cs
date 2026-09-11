using System;
using System.Collections.Generic;

namespace T2508M_SEM_Test.Models;

public partial class Rentaldetail
{
    public int RentalDetailId { get; set; }

    public int RentalId { get; set; }

    public int ComicBookId { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerDay { get; set; }

    public virtual Comicbook ComicBook { get; set; } = null!;

    public virtual Rental Rental { get; set; } = null!;
}
