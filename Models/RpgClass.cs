﻿using System;
using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum RpgClass
	{
		Knight = 1,
		Archer = 2,
		Cleric = 3,
	}
}
