using System;
using System.Collections.Generic;

namespace dotnet_rpg.Dtos.Fight
{
	public class FightRequestDto
	{
        public List<int> CharacterIds { get; set; } = new List<int>();

    }
}
