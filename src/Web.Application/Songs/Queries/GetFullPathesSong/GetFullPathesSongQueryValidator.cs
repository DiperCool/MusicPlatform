using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Songs.Queries.GetFullPathesSong
{
    public class GetFullPathesSongQueryValidator: AbstractValidator<GetFullPathesSongQuery>
    {
        public GetFullPathesSongQueryValidator()
        {
            RuleFor(x=>x.SongId)
                .NotEmpty();
        }
    }
}