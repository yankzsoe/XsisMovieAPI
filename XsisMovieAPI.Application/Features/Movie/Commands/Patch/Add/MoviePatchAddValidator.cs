using FluentValidation;

namespace XsisMovieAPI.Application.Features.Movie.Commands.Patch.Add {
    public class MoviePatchAddValidator : AbstractValidator<MoviePatchAddCommand> {
        public MoviePatchAddValidator() {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Id is requred");

            RuleFor(m => m.Movie.Title)
                .NotEmpty()
                .WithMessage("Tite is requred");

            RuleFor(m => m.Movie.Description)
                .NotEmpty()
                .WithMessage("Description is requred");
        }
    }
}
