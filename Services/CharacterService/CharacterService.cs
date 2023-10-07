﻿using System;
using System.Linq.Expressions;

namespace dotnet_rpg.Services.CharacterService
{

    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            //the await tag allows the use of async calls, letting the program continue onto other tasks while awaiting the database's response
            var dbCharacters = await _context.Characters.Where(c => c.User!.Id == userId).ToListAsync();
            // the => command is used to inline a function call, in this case grabbing the list of characters we got from the database and setting the Data variable to it
            //the mapper is an external tool that is used to swap from one class type to a similar one, through self-defined connections. Check the AutoMapperProfile to see those
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            //FirstOrDefault, or in this case the Async variant, will cycle through the list on the database until it hits the first, which works well in the case of IDs that aren't 
            //shared. If it can find none, it returns either a pre-defined default, or null.
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse; 
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            //SaveChangesAsync is necessary for any function that wants to modify the database
            await _context.SaveChangesAsync();
            serviceResponse.Data = 
                await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

                if (character is null)
                {
                    //This is in place to override the default exception thrown with a null reference, to give a more useful error message
                    throw new Exception($"Character with Id '{id}' not found");
                }
                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = 
                    await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if(character is null)
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
                }

                //Option to do the updating with the mapper, also requires a map be made connecting the UpdateCharacterDto with the Character class
                //_mapper.Map(updatedCharacter, character);
                
                character.Name = updatedCharacter.Name;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Class = updatedCharacter.Class;
                character.Intelligence = updatedCharacter.Intelligence;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
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
