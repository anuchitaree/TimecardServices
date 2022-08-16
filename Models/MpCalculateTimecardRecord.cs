
using System.ComponentModel.DataAnnotations;


namespace TimecardServices.Models
{
    public class MpCalculateTimecardRecord
    {
        [Key]
        public Guid Id { get; set; }

        [Required,MaxLength(7)]
        public string EmpId { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required,MaxLength(1)]
        public string Direction { get; set; }=null!;

        [Required,MaxLength(4)]
        public string MachineSn { get; set; } = null!;

    }
}
