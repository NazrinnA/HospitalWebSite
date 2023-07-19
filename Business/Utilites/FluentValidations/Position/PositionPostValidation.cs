using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.Position
{
    public  class PositionPostValidation:AbstractValidator<PositionPostDto>
    {
        public PositionPostValidation()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().MaximumLength(20).MinimumLength(3);
        }
    }
}
