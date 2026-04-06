using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Ammo
{
    public class HydratoxinDart : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydratoxin Dart");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(477);
			Projectile.penetrate = 3;
			Projectile.aiStyle = ProjAIStyleID.Arrow; 
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.CrystalDart;           
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.Blue;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			else
			{
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}

		public override void AI()
		{
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] > Main.rand.Next(5, 20))
			{
				if (Projectile.timeLeft > 40)
				{
					Projectile.timeLeft -= 20;
				}
				Projectile.ai[1] = 0f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 8f, ModContent.ProjectileType<HydratoxinDrop>(), Projectile.damage/2, Projectile.knockBack * 0.5f, Projectile.owner, 0f, 0f);
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.DustType<HydraToxin>(), 180);
		}
	}
}