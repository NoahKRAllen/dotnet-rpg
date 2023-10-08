using System;


namespace dotnet_rpg.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) 
		{ 
		
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Skill>().HasData(
				 new Skill { Id = 1, Name = "Fireball", Damage = 15 }, 
				 new Skill { Id = 2, Name = "Lightning Shock", Damage = 20 },
                 new Skill { Id = 3, Name = "Ice Spear", Damage = 20 },
                 new Skill { Id = 4, Name = "Frenzy", Damage = 15 },
                 new Skill { Id = 5, Name = "Death Coil", Damage = 25 }
                );
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Character> Characters => Set<Character>();

		public DbSet<User> Users => Set<User>();

        public DbSet<Weapon> Weapons => Set<Weapon>();

		public DbSet<Skill> Skills => Set<Skill>();
    }
}
