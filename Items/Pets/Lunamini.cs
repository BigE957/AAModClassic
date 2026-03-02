using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEM
    /// </summary>
    public class Lunamini : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lunamini"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
        }

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BabyHornet);
			AIType = ProjectileID.BabyHornet;
            Projectile.width = 48;
            Projectile.height = 42;
            
        }

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.hornet = false; // Relic from aiType
			return true;
		}


        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.dead)
			{
				modPlayer.Lunamini = false;
			}
			if (modPlayer.Lunamini)
			{
				Projectile.timeLeft = 2;
			}
        }
	}
}