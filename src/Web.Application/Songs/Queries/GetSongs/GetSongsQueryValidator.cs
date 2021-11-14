using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Songs.Queries.GetSongs
{
    public class GetSongsQueryValidator : AbstractValidator<GetSongsQuery>
    {
        public GetSongsQueryValidator()
        {
            RuleFor(x=>x.AlbumId)
                .NotEmpty();
            RuleFor(x=>x.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(x=>x.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}