using System;
namespace MonsterHunterAPI.Models
{
	public class Player
	{
		public int PlayerID { get; set; }
		public string PlayerName { get; set; }
		public Weapon? Weapon { get; set; }
	}
}

