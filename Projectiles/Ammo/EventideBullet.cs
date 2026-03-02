using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Ammo
{
    public class EventideBullet : ModProjectile
	{
		public override void SetStaticDefaults() 
                {
			// DisplayName.SetDefault("Eventide Bullet");     
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;    
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;        
		}

		public override void SetDefaults() 
        {
			Projectile.width = 10;               
			Projectile.height = 8;              
			Projectile.aiStyle = ProjAIStyleID.Arrow;             
			Projectile.friendly = true;         
			Projectile.hostile = false;        
			Projectile.DamageType = DamageClass.Ranged;           
			Projectile.penetrate = 50; 
                        Projectile.usesLocalNPCImmunity = false;        
			Projectile.timeLeft = 600;          
			Projectile.alpha = 255;             
			Projectile.light = 0.5f;            
			Projectile.ignoreWater = true;           
			Projectile.tileCollide = true;          
			Projectile.extraUpdates = 10;            
			AIType = ProjectileID.Bullet;      
		}

        Vector2? initialPos = null;
        Vector2? initialVel = null;
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (103 - Projectile.alpha) * 1f / 100f, (0 - Projectile.alpha) * 1f / 0f, (100 - Projectile.alpha) * 1f / 100f);
            if (initialPos == null && initialVel == null)
            {
                initialPos = Projectile.position;
                initialVel = Projectile.velocity;
            }
        }
        public int dontDrawDelay = 2;

        public override bool PreDraw(ref Color lightColor)
        { 
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) 
            {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }

            dontDrawDelay = Math.Max(0, dontDrawDelay - 1);
            return dontDrawDelay == 0;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 1;
            target.AddBuff(Mod.Find<ModBuff>("Moonraze").Type, 500);

            if (target.defense < 300 && !target.boss)
            {
                damage += target.defense * 2;
            }
            {
                int num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<YamataDust>(), -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 1.5f;
                num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<YamataDust>(), -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num580].velocity *= 1.5f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (initialPos != null && initialVel != null)
            {
                Projectile.NewProjectile((Vector2)initialPos, (Vector2)initialVel, Mod.Find<ModProjectile>("EventideBullet1").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            int num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<YamataDust>(), -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[num580].noGravity = true;
            Main.dust[num580].velocity *= 1.5f;
            num580 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<YamataDust>(), -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100);
            Main.dust[num580].velocity *= 1.5f;
        }
    }
}