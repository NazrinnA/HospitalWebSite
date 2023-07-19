using Entities.Dtos.Holiday;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.Holiday
{
    public class HolidayPostValidation:AbstractValidator<HolidayPostDto>
    {
        public HolidayPostValidation()
        {
            RuleFor(h => h.Permission).NotEmpty().NotNull();
        }
    }
}
