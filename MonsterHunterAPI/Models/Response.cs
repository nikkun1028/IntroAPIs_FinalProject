using System;
namespace MonsterHunterAPI.Models
{
	public class ResponsePlayer
	{
		public int statusCode { get; set; }
		public string statusDescription { get; set; }
		public List<Player> players { get; set; } = new();
	}

	public class ResponseWeapon
	{
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public List<Weapon> weapons { get; set; } = new();
    }

	public class ResponseWeaponType
	{
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public List<WeaponType> weaponTypes { get; set; } = new();
    }
}

