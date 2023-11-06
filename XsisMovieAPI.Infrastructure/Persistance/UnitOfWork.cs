﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XsisMovieAPI.Application.Interfaces;
using XsisMovieAPI.Application.Interfaces.Repositories;
using XsisMovieAPI.Infrastructure.Persistance.Repositories;

namespace XsisMovieAPI.Infrastructure.Persistance {
    public class UnitOfWork : IUnitOfWork {
        private readonly AppDbContext _context;

        public IMovieRepository Movie { get; private set; }

        public UnitOfWork(AppDbContext context) {
            _context = context;
            Movie = new MovieRepository(context);
        }

        public async Task<int> CompleteAsync() {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync() {
            await _context.DisposeAsync();
        }
    }
}
