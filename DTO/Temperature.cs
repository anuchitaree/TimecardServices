using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardServices.DTO
{
    internal class Temperature
    {
        public DateTime date { get; set; }
        public int temperatureC { get; set; }
        public int temperatureF { get; set; }
        public string summary { get; set; } = null!;
    }
}
