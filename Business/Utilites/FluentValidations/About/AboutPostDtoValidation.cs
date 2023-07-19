using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.About
{
    public class AboutPostDtoValidation : AbstractValidator<AboutPostDto>
    {
        public AboutPostDtoValidation()
        {
            RuleFor(a => a.Title).NotEmpty().NotNull().MaximumLength(50).MinimumLength(10);
            RuleFor(a => a.Description).NotEmpty().NotNull().MaximumLength(10000).MinimumLength(100);
            RuleFor(a => a.formFile).NotEmpty().NotNull();
        }
    }
}
