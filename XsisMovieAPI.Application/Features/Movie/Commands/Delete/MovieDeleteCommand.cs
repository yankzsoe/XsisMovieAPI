﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XsisMovieAPI.Application.Common.Exceptions;
using XsisMovieAPI.Application.Common.Models.Responses;
using XsisMovieAPI.Application.Interfaces;

namespace XsisMovieAPI.Application.Features.Movie.Commands.Delete {
    public class MovieDeleteCommand : IRequest<Response<string>> {
        public int Id { get; set; }
    }

    public class MovieDeleteCommandHandler : IRequestHandler<MovieDeleteCommand, Response<string>> {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(MovieDeleteCommand request, CancellationToken cancellationToken) {
            var data = await _unitOfWork.Movie.GetAsync(request.Id);
            if (data == null) {
                throw new NotFoundException($"Movie with ID: {request.Id} Not Found");
            }

            _unitOfWork.Movie.Remove(data);

            await _unitOfWork.CompleteAsync();

            return new Response<string>("Movie has been deleted successfully");
        }
    }
}
