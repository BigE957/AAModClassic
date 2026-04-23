using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.__Hardmode.Items._BossDukeFishron.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEM
    /// </summary>
    public class Seashroom_Sharkron : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sharkron"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
        }

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			AIType = ProjectileID.ZephyrFish;
            Projectile.width = 66;
            Projectile.height = 56;
            
        }

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.zephyrfish = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.dead)
			{
				modPlayer.Sharkron = false;
			}
			if (modPlayer.Sharkron)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}