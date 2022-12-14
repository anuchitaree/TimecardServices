using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TimecardServices.Models
{
    public class LogRecord
    {

        [Key, MaxLength(40)]
        public string Id { get; set; } = null!;

        [Required]
        public DateTime Registdatetime { get; set; }

        [Required, MaxLength(5)]
        public string Result { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Decription { get; set; } = null!;
    }
}
