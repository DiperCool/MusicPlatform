using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Subscribers.Commands.SubscribeArtist
{
    public class SubscribeArtistCommandValidator: AbstractValidator<SubscribeArtistCommand>
    {
        public SubscribeArtistCommandValidator()
        {
            RuleFor(x=>x.ArtistId)
                .NotEmpty();
        }
    }
}