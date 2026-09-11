using System.ComponentModel.DataAnnotations;

namespace ACMF_Final.Web.ViewModels
{
    public class ReportViewModel
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public List<ACMF_Final.Application.DTOs.ReportDTO> Results { get; set; } = new List<ACMF_Final.Application.DTOs.ReportDTO>();
    }
}
