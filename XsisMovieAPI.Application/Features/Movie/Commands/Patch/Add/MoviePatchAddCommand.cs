using AutoMapper;
using MediatR;
using XsisMovieAPI.Application.Common.Exceptions;
using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Application.Common.Models.Responses;
using XsisMovieAPI.Application.Interfaces;

namespace XsisMovieAPI.Application.Features.Movie.Commands.Patch.Add {
    public class MoviePatchAddCommand : IRequest<Response<string>> {
        public int Id { get; set; }
        public CreateUpdateMovie Movie { get; set; }
    }

    public class MoviePatchAddCommandHandler : IRequestHandler<MoviePatchAddCommand, Response<string>> {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoviePatchAddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(MoviePatchAddCommand request, CancellationToken cancellationToken) {
            var movie = _mapper.Map<Domain.Entities.Movie>(request.Movie);
            await _unitOfWork.Movie.AddAsync(movie);
            await _unitOfWork.CompleteAsync();
            return new Response<string>("Movie Patch Successfully");
        }
    }
}
