using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Models.Domain;
using MovieApi.Models.DTO.Characters;
using MovieApi.Models.DTO.Franchises;
using MovieApi.Models.DTO.Movies;
using MovieApi.Services.Franchises;

namespace MovieApi.Controllers.Franchises
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFranchiseService _franchiseService;

        public FranchiseController(IMapper mapper, IFranchiseService franchiseService)
        {
            _mapper = mapper;
            _franchiseService = franchiseService;
        }

        /// <summary>
        /// Gets all franchises.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetFranchises()
        {
            return Ok(_mapper.Map<List<FranchiseReadDTO>>(await _franchiseService.GetAllAsync()));
        }

        /// <summary>
        /// Gets a specific franchise by a given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id/index of specific franchise in database.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseReadDTO>(
                        await _franchiseService.GetByIdAsync(id))
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
        /// Update a specific franchise with given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id/index of specific franchise in database.</param>
        /// <param name="franchise">The new FranchiseEditDTO to apply changes with.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseEditDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            try
            {
                await _franchiseService.UpdateAsync(
                        _mapper.Map<Franchise>(franchise)
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
        /// Creates a new franchise.
        /// </summary>
        /// <param name="franchiseDto">The FranchiseCreateDTO to create from.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostFranchise(FranchiseCreateDTO franchiseDto)
        {
            Franchise franchise = _mapper.Map<Franchise>(franchiseDto);
            await _franchiseService.AddAsync(franchise);
            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);
        }

        /// <summary>
        /// Delete a specific franchise by a given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id/index of specific franchise in database.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                await _franchiseService.DeleteByIdAsync(id);
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
        /// Gets all the movies of a specific franchise, with a given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id/index of specific franchise in database.</param>
        /// <returns></returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMoviesForFranchise(int id)
        {
            try
            {
                return Ok(
                        _mapper.Map<List<MovieReadDTO>>(
                            await _franchiseService.GetMoviesAsync(id)
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
        /// Updates the movies in a specific franchise, by the their ids.
        /// </summary>
        /// <param name="movieIds">The ids/indicies of movies in the franchise.</param>
        /// <param name="id">The id/index of specific franchise in database.</param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMoviesForFranchise(int[] movieIds, int id)
        {
            try
            {
                await _franchiseService.UpdateMoviesAsync(movieIds, id);
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
        /// Gets all characters in a specific franchise, by id.
        /// </summary>
        /// <param name="id">The id/index of the specific franchise in database.</param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersForFranchise(int id)
        {
            try
            {
                return Ok(
                        _mapper.Map<List<CharacterReadDTO>>(
                            await _franchiseService.GetCharactersAsync(id)
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
    }
}
