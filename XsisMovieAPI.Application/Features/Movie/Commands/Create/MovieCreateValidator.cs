using FluentValidation;

namespace XsisMovieAPI.Application.Features.Movie.Commands.Create {
    public class MovieCreateValidator : AbstractValidator<MovieCreateCommand> {
        public MovieCreateValidator() {
            RuleFor(model => model.Movie.Title)
                .NotEmpty()
                .WithMessage("Tite is requred");

            RuleFor(model => model.Movie.Description)
                .NotEmpty()
                .WithMessage("Description is requred");
        }
    }
}
