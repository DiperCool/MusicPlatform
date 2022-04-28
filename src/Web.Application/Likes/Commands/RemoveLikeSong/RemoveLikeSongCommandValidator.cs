using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Likes.Commands.RemoveLikeSong
{
    public class RemoveLikeSongCommandValidator: AbstractValidator<RemoveLikeSongCommand>
    {
        public RemoveLikeSongCommandValidator()
        {
            RuleFor(x=>x.SongId)
                .NotEmpty();
        }
    }
}