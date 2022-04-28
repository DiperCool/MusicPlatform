using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Web.Application.Subscribers.Queries.GetSubscriptions
{
    public class GetSubscriptionsValidator: AbstractValidator<GetSubscriptionsQuery>
    {
        public GetSubscriptionsValidator()
        {
            RuleFor(x=>x.PageSize)
                .GreaterThanOrEqualTo(1);
            RuleFor(x=>x.PageNumber)
                .GreaterThanOrEqualTo(1);
        }
    }
}