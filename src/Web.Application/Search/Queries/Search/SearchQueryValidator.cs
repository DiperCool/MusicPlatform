using FluentValidation;

namespace Web.Application.Search.Queries.Search;
public class SearchQueryValidator: AbstractValidator<SearchQuery>
{
    public SearchQueryValidator()
    {
       RuleFor(x=>x.Id)
            .NotEmpty();
        RuleFor(x=>x.PageSize)
            .GreaterThanOrEqualTo(1);
            
    }
}