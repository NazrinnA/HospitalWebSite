using Entities.Concrete.Models;
using Entities.Dtos.Holiday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IHolidayService
    {
        Task<HolidayGetDto> Get();
        Task Update(HolidayPostDto holiday);
         Task<bool> Check();

    }
}
