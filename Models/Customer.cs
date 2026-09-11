using System;
using System.Collections.Generic;

namespace T2508M_SEM_Test.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
