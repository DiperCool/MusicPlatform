using FluentValidation;

namespace Web.Application.Albums.Commands.CreateAlbum;
public class CreateAlbumCommandValidator: AbstractValidator<CreateAlbumCommand>
{   
    public CreateAlbumCommandValidator()
    {
        RuleFor(x=>x.Title)
            .NotEmpty()
            .MaximumLength(20);
        RuleFor(x=>x.File)
            .NotNull();
    } 
}