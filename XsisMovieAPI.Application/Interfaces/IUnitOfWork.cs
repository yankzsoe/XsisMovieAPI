using XsisMovieAPI.Application.Interfaces.Repositories;

namespace XsisMovieAPI.Application.Interfaces {
    public interface IUnitOfWork : IAsyncDisposable {
        IMovieRepository Movie { get; }

        Task<int> CompleteAsync();
    }
}
