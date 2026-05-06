using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Pets
{
    public class CyberneticEgg_Raidmini : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broodmini"); 
			Main.projFrames[Projectile.type] = 3;
			Main.projPet[Projectile.type] = true;
        }

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.DD2PetDragon);
			AIType = ProjectileID.DD2PetDragon;
            Projectile.width = 66;
            Projectile.height = 56;
        }

        public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.petFlagDD2Dragon = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.dead)
			{
				modPlayer.Raidmini = false;
			}
			if (modPlayer.Raidmini)
			{
				Projectile.timeLeft = 2;
			}
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                {
                    Projectile.frame = 0;
                }
            }
        }
	}
}