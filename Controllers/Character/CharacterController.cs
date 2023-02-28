using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.DTO.Character;
using System.Net.Mime;
using AutoMapper;

namespace MovieApi.Controllers.Character
{
    [Route("api/v1/character")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharacterController : ControllerBase
    {
        protected readonly MovieDbContext? _context;
        protected readonly IMapper ?_mapper;

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
            return _mapper!.Map<List<CharacterReadDTO>>(await _context!.Character
                .Include(c => c.Movies)
                .ToListAsync());
        }
    }
}
