using XsisMovieAPI.Application.Interfaces;

namespace XsisMovieAPI.Application.Services {
    public class DateTimeService : IDateTime {
        public DateTime Now => DateTime.Now;
    }
}
