using AAModClassic.Assets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Weapons
{
    public class Fireball_Explosion : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Explosion");
		}

		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Throwing;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hostile = false;
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1;
			Projectile.tileCollide = false;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 1;
			target.AddBuff(BuffID.OnFire, 300);
		}

	}
}
