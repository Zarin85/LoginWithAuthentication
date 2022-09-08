using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using loginwithauthentication.Models;
using loginwithauthentication.Services.CharacterService;
using loginwithauthentication.Dtos.CharacterDto;
using AutoMapper;
using loginwithauthentication.Data;
using Microsoft.EntityFrameworkCore;

namespace loginwithauthentication.Services.CharacterService
{
    public class CharacterService: ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
            public CharacterService(IMapper mapper, DataContext context )
            {
                _context = context;
                _mapper = mapper;
            }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character charac = _mapper.Map<Character>(character);
            await _context.Characters.AddAsync(charac);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbcharacters = await _context.Characters.Where(c => c.User.Id == userId).ToListAsync();
            serviceResponse.Data = dbcharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbcharacter = await _context.Characters.FirstOrDefaultAsync(c=> c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbcharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
               Character charac = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
               charac.Name = updateCharacter.Name;
               charac.HitPoints = updateCharacter.HitPoints;
               charac.Strength = updateCharacter.Strength;
               charac.Defense = updateCharacter.Defense;
               charac.Intelligence = updateCharacter.Intelligence;
               charac.Class = updateCharacter.Class;

               _context.Characters.Update(charac);
               await _context.SaveChangesAsync();

               serviceResponse.Data = _mapper.Map<GetCharacterDto>(charac);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character charac = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(charac);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

    }
}