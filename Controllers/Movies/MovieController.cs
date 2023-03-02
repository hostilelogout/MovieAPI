using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models.Domain;
using MovieApi.Models.DTO.Characters;
using MovieApi.Models.DTO.Movies;
using MovieApi.Services.Movies;
using System.Net;
using System.Net.Mime;

namespace MovieApi.Controllers.Movies
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]

    [ApiController]
    public class MovieController : ControllerBase
    {
        protected readonly IMovieService _movieService;
        protected readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }


        /// <summary>
        /// Fetches all the movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
        {
            return Ok(_mapper!.Map<List<MovieReadDTO>>(await _movieService!.GetAllAsync()));
        }

        /// <summary>
        /// Gets a specific movie by their Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovieById(int id)
        {
            try
            {
                return Ok(_mapper!.Map<MovieReadDTO>(
                        await _movieService!.GetByIdAsync(id))
                    );
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Adds a new movie to the database.
        /// </summary>
        /// <param name="movieDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovieCreateDTO>> CreateCharacter(MovieCreateDTO movieDTO)
        {
            Movie movie = _mapper!.Map<Movie>(movieDTO);
            await _movieService!.AddAsync(movie);
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }


        /// <summary>
        /// Updates a movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieEditDTO movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await _movieService.UpdateAsync(
                        _mapper.Map<Movie>(movie)
                    );
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Deletes a movies.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService!.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Gets all the characters in a specific movie, with a given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id/index of specific movie object in database.</param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInMovie(int id)
        {
            try
            {
                return Ok(
                        _mapper.Map<List<CharacterReadDTO>>(
                            await _movieService.GetCharactersAsync(id)
                        )
                    );
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Updates the characters in a specific movie, by the their ids.
        /// </summary>
        /// <param name="movieIds">The ids/indicies of characters in the movie.</param>
        /// <param name="id">The id/index of specific movie in database.</param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharactersInMovie(int[] movieIds, int id)
        {
            try
            {
                await _movieService.UpdateCharactersAsync(movieIds, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }
    }
}
