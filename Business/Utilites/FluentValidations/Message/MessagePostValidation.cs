using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilites.FluentValidations.Message
{
    public class MessagePostValidation:AbstractValidator<MessagePostDto>
    {
        public MessagePostValidation()
        {
            RuleFor(m => m.Letter).NotEmpty().NotNull();
            RuleFor(m => m.Subject).NotEmpty().NotNull().MaximumLength(20);
        }
    }
}
