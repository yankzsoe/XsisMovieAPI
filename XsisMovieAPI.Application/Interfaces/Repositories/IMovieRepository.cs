using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Application.Features.Movie.Queries.GetList;
using XsisMovieAPI.Domain.Entities;

namespace XsisMovieAPI.Application.Interfaces.Repositories {
    public interface IMovieRepository : IGenericRepository<Movie> {
        Task<Movie> GetByIdAsNoTrackingAsync(int id);
        Task<(int totalCount, List<Movie> movies)> GetListByTitleAsNoTrackingAsync(string title);
        Task<(int totalCount, List<Movie> movies)> GetListAsNoTrackingAsync(MovieGetListQuery query);
        Task<Movie> UpdateMovieAsync(int id, MovieViewModel movie);
    }
}
