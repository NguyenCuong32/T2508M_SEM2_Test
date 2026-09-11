using System;

namespace ComicSystem.ViewModels
{
    public class ReportItemViewModel
    {
        public int No { get; set; }
        public string BookName { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }

        public int RentalDays
        {
            get
            {
                var days = (ReturnDate.Date - RentalDate.Date).Days;
                return days <= 0 ? 1 : days;
            }
        }

        public decimal EstimatedTotal => PricePerDay * Quantity * RentalDays;
    }
}
