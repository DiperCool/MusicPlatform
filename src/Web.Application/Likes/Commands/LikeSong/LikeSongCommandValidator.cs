using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Likes.Commands.LikeSong
{
    public class LikeSongCommandValidator: AbstractValidator<LikeSongCommand>
    {
        public LikeSongCommandValidator()
        {
            RuleFor(x=>x.SongId)
                .NotEmpty();
        }
    }
}