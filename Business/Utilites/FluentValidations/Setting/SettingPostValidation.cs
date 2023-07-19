using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.Setting
{
    public class SettingPostValidation : AbstractValidator<SettingPostDto>
    {
        public SettingPostValidation()
        {
            RuleFor(s => s.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(s => s.Address).NotNull().NotEmpty().MaximumLength(50).MinimumLength(10);
            RuleFor(s => s.Phone).NotNull().NotEmpty().MaximumLength(18).MinimumLength(9);
            RuleFor(s => s.Logo).NotNull().NotEmpty().MaximumLength(20).MinimumLength(3);
        }

    }
}
