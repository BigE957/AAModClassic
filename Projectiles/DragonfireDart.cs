using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DragonfireDart : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragonfire Dart");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(477);
			Projectile.penetrate = 1;
			Projectile.aiStyle = ProjAIStyleID.Arrow; 
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.CrystalDart;           
		}
		
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red;
        }

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.NPCHit7, Projectile.position);
			for (int h = 0; h < 5; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				int type = Main.rand.Next(326,328);
				int proj = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, type, Projectile.damage, 0, Main.myPlayer);
				Main.projectile[proj].localNPCHitCooldown = -1;
				Main.projectile[proj].timeLeft = 30;
				Main.projectile[proj].hostile = false;
				Main.projectile[proj].friendly = true;
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
				spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Mod.Find<ModDust>("DragonFire").Type, 180);
		}
	}
}