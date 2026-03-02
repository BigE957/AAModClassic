using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEM
    /// </summary>
    public class Broodmini : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broodmini"); // Automatic from .lang files
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
				modPlayer.Broodmini = false;
			}
			if (modPlayer.Broodmini)
			{
				Projectile.timeLeft = 2;
			}
        }
	}
}