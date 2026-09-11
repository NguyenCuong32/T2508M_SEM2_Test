using System;
using System.Collections.Generic;

namespace T2508M_SEM_Test.Models;

public partial class Comicbook
{
    public int ComicBookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal PricePerDay { get; set; }

    public virtual ICollection<Rentaldetail> Rentaldetails { get; set; } = new List<Rentaldetail>();
}
