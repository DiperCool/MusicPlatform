using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Likes.Queries.GetLikes
{
    public class GetLikesQueryValidator: AbstractValidator<GetLikesQuery>
    {
        public GetLikesQueryValidator()
        {
            RuleFor(x=>x.PageSize)
                .GreaterThanOrEqualTo(1);
            RuleFor(x=>x.PageNumber)
                .GreaterThanOrEqualTo(1);
        }
    }
}