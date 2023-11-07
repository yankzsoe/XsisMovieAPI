using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XsisMovieAPI.Application.Common.Exceptions;
using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Application.Common.Models.Responses;
using XsisMovieAPI.Application.Interfaces;

namespace XsisMovieAPI.Application.Features.Movie.Commands.Patch.Update {
    public class MoviePatchUpdateCommand : IRequest<Response<string>> {
        public int Id { get; set; }
        public CreateUpdateMovie Movie { get; set; }
    }

    public class MoviePatchUpdateCommandHandler : IRequestHandler<MoviePatchUpdateCommand, Response<string>> {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoviePatchUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(MoviePatchUpdateCommand request, CancellationToken cancellationToken) {
            var data = await _unitOfWork.Movie.GetAsync(request.Id);

            if (data == null) {
                throw new NotFoundException($"Movie with ID: {request.Id} Not Found");
            }

            if (!string.IsNullOrWhiteSpace(request.Movie.Title)) {
                data.Title = request.Movie.Title;
            }

            if (!string.IsNullOrWhiteSpace(request.Movie.Description)) {
                data.Description = request.Movie.Description;
            }

            if (request.Movie.Rating > 0) {
                data.Rating = request.Movie.Rating;
            }

            if (!string.IsNullOrWhiteSpace(request.Movie.Image)) {
                data.Image = request.Movie.Image;
            }

            await _unitOfWork.CompleteAsync();
            return new Response<string>("Movie Patch Successfully");
        }
    }
}
