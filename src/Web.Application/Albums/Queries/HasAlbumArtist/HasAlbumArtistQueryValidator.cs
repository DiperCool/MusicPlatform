using FluentValidation;

namespace Web.Application.Albums.Queries.HasAlbumArtist
{
    public class HasAlbumArtistQueryValidator: AbstractValidator<HasAlbumArtistQuery>
    {
        public HasAlbumArtistQueryValidator()
        {
            RuleFor(x=>x.AlbumId)
                .NotEmpty();
        }
    }
}