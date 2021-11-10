using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Albums.Commands.UpdatePicturesAlbum
{
    public class UpdatePicturesAlbumCommandValidator: AbstractValidator<UpdateAlbumCommand>
    {
        public UpdatePicturesAlbumCommandValidator()
        {
            RuleFor(x=>x.AlbumId)
                .NotEmpty();
        }
    }
}