using AAModClassic.___Content.Inferno.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class DragonfireBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragonfire Bullet");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = ProjAIStyleID.Arrow; 
			Projectile.friendly = true;    
			Projectile.hostile = false;     
			Projectile.DamageType = DamageClass.Ranged;         
			Projectile.timeLeft = 600; 
			Projectile.alpha = 255;  
			Projectile.light = 0f; 
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;           
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

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 90);
		}
	}
}