using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models.Domain;
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
        protected readonly IMovieService ? _context;
        protected readonly IMapper ? _mapper;

        public MovieController(IMovieService ? context, IMapper ? mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Fetches all the movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
        {
            return Ok(_mapper!.Map<List<MovieReadDTO>>(await _context!.GetAllAsync()));
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
                        await _context!.GetByIdAsync(id))
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
            await _context!.AddAsync(movie);
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
            return null;
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
                await _context!.DeleteByIdAsync(id);
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
