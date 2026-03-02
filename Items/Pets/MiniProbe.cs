using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEM
    /// </summary>
    public class MiniProbe : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mini Probe"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 6;
			Main.projPet[Projectile.type] = true;
        }

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.SuspiciousTentacle);
            AIType = ProjectileID.SuspiciousTentacle;
            Projectile.width = 14;
            Projectile.height = 14;
            
        }

        public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.suspiciouslookingTentacle = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.dead)
			{
				modPlayer.MiniProbe = false;
			}
			if (modPlayer.MiniProbe)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}