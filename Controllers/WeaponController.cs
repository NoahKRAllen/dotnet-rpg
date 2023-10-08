using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Weapon;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace dotnet_rpg.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class WeaponController : ControllerBase
	{
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }
    }
}
