using System;
namespace MonsterHunterAPI.Models
{
	public class Weapon
	{
		public int WeaponID { get; set; }
		public WeaponType? WeaponType { get; set; }
		public string WeaponName { get; set; }
		public int ATK { get; set; }
		public int Critical { get; set; }
	}
}

