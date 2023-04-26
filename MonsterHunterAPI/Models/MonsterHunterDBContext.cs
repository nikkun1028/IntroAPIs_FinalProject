using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using MonsterHunterAPI.Models;

namespace MonsterHunterAPI.Models
{
	public class MonsterHunterDBContext : DbContext
	{
		protected readonly IConfiguration Configuration;

		public MonsterHunterDBContext(DbContextOptions<MonsterHunterDBContext> options,
			IConfiguration configuration) : base(options)
		{
			Configuration = configuration;
		}


		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			var connectionString = Configuration.GetConnectionString("MonsterHunter");
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}


		public DbSet<Player> Players { get; set; } = null!;
		public DbSet<Weapon> Weapons { get; set; } = null!;
		public DbSet<WeaponType> WeaponTypes { get; set; } = null!;
	}
}

