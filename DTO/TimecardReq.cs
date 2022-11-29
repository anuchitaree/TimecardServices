using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardServices.DTO
{
    public class TimecardReq
    {
    
        public string Id { get; set; } = null!;

        public string EmpId { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string Direction { get; set; } = null!;

        public string MachineSn { get; set; } = null!;
    }
}
