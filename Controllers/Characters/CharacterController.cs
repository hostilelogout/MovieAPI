using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.DTO.Characters;
using System.Net.Mime;
using AutoMapper;
using MovieApi.Models.Domain;

namespace MovieApi.Controllers.Characters
{
    [Route("api/v1/character")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharacterController : ControllerBase
    {
        protected readonly MovieDbContext? _context;
        protected readonly IMapper? _mapper;

        public CharacterController(MovieDbContext? context, IMapper? mapper)
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
            return Ok(await _context!.Character.ToListAsync());
        }

        /// <summary>
        /// Gets a specific character by their Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacterById(int id)
        {
            Character? character = await _context!.Character.FindAsync(id);

            if (character == null) { return NotFound(); }

            return Ok(character);
        }

        /// <summary>
        /// Adds a new character to the database.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CharacterCreateDTO>> CreateCharacter(Character character)
        {
            _context?.Character.Add(character);
            await _context!.SaveChangesAsync();

            return CreatedAtAction("GetCharacter",new {id = character.Id},character);
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
            if (id != character.Id) {  return BadRequest(); }

            Character ? domainCharacter = _mapper!.Map<Character>(character);
            _context!.Entry(domainCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id)) return NotFound();
                else { throw; }
            }

           return NoContent();
        }

        /// <summary>
        /// Deletes a character.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            Character ? character = await _context!.Character.FindAsync(id);
            if (character == null) { return NotFound(); }

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected bool CharacterExists(int id) => _context!.Character.Any(x => x.Id == id);
    }
}
