using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using loginwithauthentication.Models;
using loginwithauthentication.Dtos.CharacterDto;

namespace loginwithauthentication.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}