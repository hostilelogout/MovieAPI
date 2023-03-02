﻿using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.DTO.Characters;
using System.Net.Mime;
using AutoMapper;
using MovieApi.Models.Domain;
using MovieApi.Services.Characters;
using MovieApi.Models.DTO.Franchises;
using System.Net;

namespace MovieApi.Controllers.Characters
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]

    [ApiController]
    public class CharacterController : ControllerBase
    {
        protected readonly ICharacterService ? _context;
        protected readonly IMapper ? _mapper;

        public CharacterController(ICharacterService ? context, IMapper ? mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all the Characters and the movies they appear in.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters()
        {
            return Ok(_mapper!.Map<List<CharacterReadDTO>>(await _context!.GetAllAsync()));
        }

        /// <summary>
        /// Gets a specific character by their Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacterById(int id)
        {
            try
            {
                return Ok(_mapper!.Map<CharacterReadDTO>(
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
        /// Adds a new character to the database.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CharacterCreateDTO>> CreateCharacter(Character character)
        {
            return null;
        }


        /// <summary>
        /// Updates a character.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, CharacterEditDTO character)
        {
            return null;
        }

        /// <summary>
        /// Deletes a character.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
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
