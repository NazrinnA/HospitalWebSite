using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.Service
{
    public class ServicePostValidation:AbstractValidator<ServicePostDto>
    {
        public ServicePostValidation()
        {
            RuleFor(s=>s.Title).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);
            RuleFor(s=>s.Description).NotEmpty().NotNull().MaximumLength(10000).MinimumLength(10);

        }
    }
}
