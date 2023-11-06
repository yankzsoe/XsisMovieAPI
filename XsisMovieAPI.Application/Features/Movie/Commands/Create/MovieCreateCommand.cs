using AutoMapper;
using FluentValidation;
using MediatR;
using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Application.Common.Models.Responses;
using XsisMovieAPI.Application.Interfaces;

namespace XsisMovieAPI.Application.Features.Movie.Commands.Create {
    public class MovieCreateCommand : IRequest<Response<MovieViewModel>> {
        public CreateUpdateMovie Movie { get; set; }
    }

    public class MovieCreateCommandHandler : IRequestHandler<MovieCreateCommand, Response<MovieViewModel>> {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<MovieViewModel>> Handle(MovieCreateCommand request, CancellationToken cancellationToken) {
            var movie = _mapper.Map<MovieViewModel>(request.Movie);
            var data = await _unitOfWork.Movie.InsertMovieAsync(movie);
            var result = _mapper.Map<MovieViewModel>(data);
            return new Response<MovieViewModel>(result, "Created Successfully");
        }
    }
}
