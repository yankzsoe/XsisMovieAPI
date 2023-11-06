using Microsoft.EntityFrameworkCore;
using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Application.Common.Models.Enums;
using XsisMovieAPI.Application.Features.Movie.Queries.GetList;
using XsisMovieAPI.Application.Interfaces.Repositories;
using XsisMovieAPI.Domain.Entities;

namespace XsisMovieAPI.Infrastructure.Persistance.Repositories {
    public class MovieRepository : GenericRepository<MovieRepository>, IMovieRepository {
        public MovieRepository(AppDbContext context) : base(context) { }

        public AppDbContext AppDbContext {
            get {
                return Context as AppDbContext;
            }
        }

        public async Task<(int totalCount, List<Movie> movies)> GetListAsNoTrackingAsync(MovieGetListQuery query) {
            IQueryable<Movie> data = AppDbContext.Movies;

            if (!string.IsNullOrWhiteSpace(query.Keyword)) {
                data.Where(e => e.Title == query.Keyword);
            }

            if (query.MovieGetListOrderBy == MovieGetListOrderBy.Id) {
                if (query.SortBy == SortBy.Asc) {
                    data.OrderBy(e => e.Id);
                } else {
                    data.OrderByDescending(e => e.Id);
                }
            }

            if (query.MovieGetListOrderBy == MovieGetListOrderBy.Title) {
                if (query.SortBy == SortBy.Asc) {
                    data.OrderBy(e => e.Title);
                } else {
                    data.OrderByDescending(e => e.Title);
                }
            }

            if (query.MovieGetListOrderBy == MovieGetListOrderBy.CreatedDate) {
                if (query.SortBy == SortBy.Asc) {
                    data.OrderBy(e => e.CreatedDate);
                } else {
                    data.OrderByDescending(e => e.CreatedDate);
                }
            }

            int totalCount = await data.AsNoTracking().CountAsync();
            
            var list = await data
                .AsNoTracking()
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (totalCount, list);
        }

        public Task<(int totalCount, List<Movie> movies)> GetListByTitleAsNoTrackingAsync(string title) {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetByIdAsNoTrackingAsync(int id) {
            return await AppDbContext.Movies
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie> GetByIdAsync(int id) {
            return await AppDbContext.Movies
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie> InsertMovieAsync(MovieViewModel movie) {
            var data = new Movie() {
              Title = movie.Title,
              Description = movie.Description,
              Rating = movie.Rating,
              Image = movie.Image,
            };

            await AppDbContext.Movies.AddAsync(data);
            await AppDbContext.SaveChangesAsync();
            return data;
        }

        public async Task<Movie> UpdateMovieAsync(int id, MovieViewModel movie) {
            var data = new Movie() {
                Id = id,
                Title = movie.Title,
                Description = movie.Description,
                Rating = movie.Rating,
                Image = movie.Image,
            };

            AppDbContext.Movies.Update(data);
            await AppDbContext.SaveChangesAsync();
            return data;
        }
    }
}
