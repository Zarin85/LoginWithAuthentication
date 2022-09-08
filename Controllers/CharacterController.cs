using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using loginwithauthentication.Models;
using loginwithauthentication.Services.CharacterService;
using loginwithauthentication.Dtos.CharacterDto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace loginwithauthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
       private readonly ICharacterService _characterService;

       public CharacterController(ICharacterService characterService)
       {
            _characterService = characterService;
       }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters(userId));
        }

        [HttpPost("AddCharacter")]
        public async Task<IActionResult> AddCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut("UpdateCharacter")]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto character)
        {
            ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(character);
            if(response.Data != null)
                return Ok(response);
            else
            {
                return NotFound(response);
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);
            if(response.Data != null)
                return Ok(response);
            else
            {
                return NotFound(response);
            }
        }
    }
}