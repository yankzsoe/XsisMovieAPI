using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using XsisMovieAPI.Application.Common.Models;
using XsisMovieAPI.Application.Common.Models.Responses;
using XsisMovieAPI.Application.Features.Movie.Commands.Create;
using XsisMovieAPI.Application.Features.Movie.Commands.Update;
using XsisMovieAPI.Application.Features.Movie.Queries.Get;
using XsisMovieAPI.Application.Features.Movie.Queries.GetList;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Linq;

namespace XsisMovieAPI.WebAPI.Controllers {
    [Route("api/Movies")]
    public class MoviesController : ApiControllerBase {

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<MovieViewModel>>>> GetListMovie([FromQuery] MovieGetListQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("/{Id:int}")]
        public async Task<ActionResult<PagedResponse<List<MovieViewModel>>>> GetMovie(int Id) {
            var query = new MovieGetQuery { Id = Id };
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<Response<MovieViewModel>>> CreateMovie(MovieCreateCommand command) {
            return Ok(await Mediator.Send(command));
        }

        [HttpPatch("/{ID:int}")]
        public async Task<ActionResult<Response<MovieViewModel>>> UpdateMovie(int ID, [FromBody] JsonPatchDocument<PatchMovie> patch) {
            //var newMovie = new CreateUpdateMovie();
            //if (patch is not null) {
            //    foreach (Operation<PatchMovie> operation in patch.Operations) {
            //        switch (operation.OperationType) {
            //            case OperationType.Add:
            //                JToken token = JToken.FromObject(operation.value);
            //                newMovie = token.ToObject<CreateUpdateMovie>();
            //                break;
            //        default:
            //                break;
            //        }

            //    }
            //}

            var command = new MovieUpdateCommand { Id = ID };
            var movie = new PatchMovie();
            patch.ApplyTo(movie);
            command.Movie = movie;
            //using (var reader = new StreamReader(Request.Body)) {
            //    var requestBody = reader.ReadToEnd();
            //    var data = JsonSerializer.Deserialize<CreateUpdateMovie>(requestBody);
            //    command.Movie = data;
            //}
            return Ok(await Mediator.Send(command));
        }
    }
}
