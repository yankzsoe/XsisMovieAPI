using Microsoft.AspNetCore.Mvc;
using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Application.Common.Models.Responses;
using XsisMovieAPI.Application.Features.Movie.Commands.Create;
using XsisMovieAPI.Application.Features.Movie.Commands.Update;
using XsisMovieAPI.Application.Features.Movie.Queries.Get;
using XsisMovieAPI.Application.Features.Movie.Queries.GetList;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Linq;
using XsisMovieAPI.Application.Features.Movie.Commands.Delete;
using XsisMovieAPI.Application.Features.Movie.Commands.Patch.Add;
using XsisMovieAPI.Application.Features.Movie.Commands.Patch.Update;

namespace XsisMovieAPI.WebAPI.Controllers {
    [Route("/Movies")]
    public class MoviesController : ApiControllerBase {

        [HttpGet("/")]
        public async Task<ActionResult<PagedResponse<List<MovieViewModel>>>> GetListMovie([FromQuery] MovieGetListQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("/{ID:int}")]
        public async Task<ActionResult<PagedResponse<List<MovieViewModel>>>> GetMovie(int ID) {
            var query = new MovieGetQuery { Id = ID };
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<Response<MovieViewModel>>> CreateMovie(MovieCreateCommand command) {
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("/{ID:int}")]
        public async Task<ActionResult<Response<string>>> UpdateMovie(int ID, [FromBody] JsonPatchDocument<CreateUpdateMovie> patch) {
            var newMovie = new CreateUpdateMovie();
            if (patch is not null) {
                foreach (Operation<CreateUpdateMovie> operation in patch.Operations) {
                    JToken token = JToken.FromObject(operation.value);
                    newMovie = token.ToObject<CreateUpdateMovie>();

                    switch (operation.OperationType) {
                        case OperationType.Add:
                            var add = new MoviePatchAddCommand() { 
                                Id = ID, 
                                Movie = new CreateUpdateMovie() {
                                    Title = newMovie.Title,
                                    Description = newMovie.Description,
                                    Image = newMovie.Image,
                                    Rating = newMovie.Rating
                                }
                            };
                            return Ok(await Mediator.Send(add));

                        case OperationType.Replace:
                            var update = new MoviePatchUpdateCommand() {
                                Id = ID,
                                Movie = new CreateUpdateMovie() {
                                    Title = newMovie.Title,
                                    Description = newMovie.Description,
                                    Image = newMovie.Image,
                                    Rating = newMovie.Rating
                                }
                            };
                            return Ok(await Mediator.Send(update));
                        default:
                            break;
                    }

                }
            }

            var command = new MoviePatchUpdateCommand { Id = ID };
            var movie = new CreateUpdateMovie();
            patch.ApplyTo(movie);
            command.Movie = movie;
            
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("/{ID:int}")]
        public async Task<ActionResult<Response<string>>> DeleteMovie(int ID) {
            var command = new MovieDeleteCommand { Id = ID };
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("/{ID:int}")]
        public async Task<ActionResult<Response<string>>> UpdateMovie(int ID, [FromBody] CreateUpdateMovie request) {

            var command = new MovieUpdateCommand() {
                Id = ID,
                Title = request.Title,
                Description = request.Description,
                Rating = request.Rating,
                Image = request.Image
            };

            return Ok(await Mediator.Send(command));
        }
    }
}
