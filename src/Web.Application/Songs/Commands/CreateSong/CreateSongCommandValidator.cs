using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Songs.Commands.CreateSong
{
    public class CreateSongCommandValidator : AbstractValidator<CreateSongCommand>
    {
        public CreateSongCommandValidator()
        {
            RuleFor(x=>x.AlbumId)
                .NotEmpty();
            RuleFor(x=>x.Title)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(x=>x.File)
                .NotNull();
        }
    }
}