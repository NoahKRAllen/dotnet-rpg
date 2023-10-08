using System;

namespace dotnet_rpg.Dtos.Character
{
    //DTO stands for Data Transfer Object

    public class GetCharacterDto
	{
        public int Id { get; set; }

        public string Name { get; set; } = "Frodo";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Archer;

        public GetWeaponDto? Weapon { get; set; }

        public List<GetSkillDto>? Skills { get; set; }

        public int Fights { get; set; }

        public int Victories { get; set; }

        public int Defeats { get; set; }
    }
}
