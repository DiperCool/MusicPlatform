using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Songs.Commands.UpdateSong
{
    public class UpdateSongCommandValidator : AbstractValidator<UpdateSongCommand>
    {
        public UpdateSongCommandValidator()
        {
            RuleFor(x=>x.SongId)
                .NotEmpty();

        }
    }
}