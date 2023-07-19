using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.Doctor
{
    public class DoctorPostDtoValidation : AbstractValidator<DoctorPostDto>
    {
        public DoctorPostDtoValidation()
        {
            RuleFor(d => d.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(d => d.Phone).NotEmpty().NotNull();
            RuleFor(d => d.About).NotEmpty().NotNull();
            RuleFor(d => d.Surname).NotEmpty().NotNull();
            RuleFor(d => d.Phone).NotEmpty().NotNull();
            RuleFor(d => d.Name).NotEmpty().NotNull().MaximumLength(10).MinimumLength(3);

        }
    }
}
