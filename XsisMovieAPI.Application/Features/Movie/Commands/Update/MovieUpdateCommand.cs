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

namespace XsisMovieAPI.Application.Features.Movie.Commands.Update {
    public class MovieUpdateCommand : IRequest<Response<MovieViewModel>> {
        public int Id { get; set; }
        public PatchMovie Movie { get; set; }
    }

    public class MovieUpdateCommandHandler : IRequestHandler<MovieUpdateCommand, Response<MovieViewModel>> {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<MovieViewModel>> Handle(MovieUpdateCommand request, CancellationToken cancellationToken) {
            var movie = _mapper.Map<MovieViewModel>(request.Movie.Movie);
            var data = await _unitOfWork.Movie.GetByIdAsync(request.Id);
            if (data == null) {
                throw new NotFoundException($"Movie with ID: {request.Id} Not Found");
            }

            data.Title = movie.Title;
            data.Description = movie.Description;
            data.Rating = movie.Rating;
            data.Image = movie.Image;            

            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<MovieViewModel>(data);
            return new Response<MovieViewModel>(result, "Movie Updated Successfully");
        }
    }
}
