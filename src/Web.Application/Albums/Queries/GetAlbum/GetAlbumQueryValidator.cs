using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Albums.Queries.GetAlbum;
public class GetAlbumsQueryValidator: AbstractValidator<GetAlbumQuery>
{
    public GetAlbumsQueryValidator()
    {
        RuleFor(x=>x.AlbumId)
            .NotEmpty();
    }
}