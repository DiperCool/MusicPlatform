using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Songs.Commands.DeleteSong;
public class DeleteSongCommandValidator: AbstractValidator<DeleteSongCommand>
{
    public DeleteSongCommandValidator()
    {
        RuleFor(x=>x.SongId)
            .NotEmpty();
    }
}