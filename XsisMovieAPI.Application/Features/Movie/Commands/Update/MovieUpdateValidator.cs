using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XsisMovieAPI.Application.Features.Movie.Commands.Update {
    public class MovieUpdateValidator : AbstractValidator<MovieUpdateCommand> {
        public MovieUpdateValidator() {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Id is requred");

            RuleFor(m => m.Title)
                .NotEmpty()
                .WithMessage("Tite is requred");

            RuleFor(m => m.Description)
                .NotEmpty()
                .WithMessage("Description is requred");
        }
    }
}
