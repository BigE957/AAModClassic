using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
	public class Void : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 3.5f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13f;
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 99;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 1f;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int p = Projectile.NewProjectile(Projectile.Center, new Vector2(0, 0), ProjectileID.Electrosphere, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI, 0);
            Main.projectile[p].ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Main.projectile[p].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
        }

        public override void PostAI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
				dust.noGravity = true;
            }
        }
	}
}
