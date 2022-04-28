using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Subscribers.Commands.UnsubscribeArtist
{
    public class UnsubscribeArtistCommandValidator: AbstractValidator<UnsubscribeArtistCommand>
    {
        public UnsubscribeArtistCommandValidator()
        {
            RuleFor(x=>x.ArtistId)
                .NotEmpty();
        }
    }
}