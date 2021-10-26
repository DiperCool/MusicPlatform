using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Test.Commands.SetTest
{
    public class SetTestCommandValidator: AbstractValidator<SetTestCommand>
    {
        public SetTestCommandValidator()
        {
            RuleFor(x=>x.Test)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);
        }
    }
}