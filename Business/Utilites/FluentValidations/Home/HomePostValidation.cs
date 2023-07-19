using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.Home
{
    public class HomePostValidation:AbstractValidator<HomePostDto>
    {
        public HomePostValidation()
        {
            RuleFor(h=>h.Title).NotEmpty().NotNull().MaximumLength(30).MinimumLength(5); 
            RuleFor(h=>h.Slogan).NotEmpty().NotNull().MaximumLength(60).MinimumLength(20);
            RuleFor(h => h.formFile).NotEmpty().NotNull();
        }
    }

}
