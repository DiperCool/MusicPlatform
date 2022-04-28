using FluentValidation;

namespace Web.Application.Search.Queries.Search;
public class SearchQueryValidator: AbstractValidator<SearchQuery>
{
    public SearchQueryValidator()
    {
       RuleFor(x=>x.PageNumber)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageSize)
            .GreaterThanOrEqualTo(1);
            
    }
}