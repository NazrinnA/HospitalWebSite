using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Holiday
{
    public class HolidayGetDto
    {
        public bool Permission { get; set; }
        public string? DoctorEmail { get; set; }
    }
}
