using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Albums.Queries.GetAlbums;
public class GetAlbumsQueryValidator: AbstractValidator<GetAlbumsQuery>
{
    public GetAlbumsQueryValidator()
    {
        RuleFor(x=>x.ArtistId)
            .NotEmpty();
        RuleFor(x=>x.PageSize)
            .GreaterThanOrEqualTo(1);
    }
}