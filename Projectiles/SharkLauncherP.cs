using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SharkLauncherP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shark");
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(190);
			Projectile.aiStyle = ProjAIStyleID.MechanicalPiranha;
			AIType = ProjectileID.MechanicalPiranha;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 4;
		}
	}
}
