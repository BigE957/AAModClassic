using AAModClassic.___Content.Mire.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.Ammo
{
    public class EventideArrow_Proj : ModProjectile
	{
        //TODO: Did this exist?
        /*
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
         {
            if (Main.netMode != NetmodeID.Server)
            {
                Asset<Texture2D>[] glowMasks = new Asset<Texture2D>[TextureAssets.GlowMask.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i];
                }
                glowMasks[glowMasks.Length - 1] = ModContent.Request<Texture2D>(Texture + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask = glowMasks;
            }
            Projectile.glowMask = customGlowMask;

            // DisplayName.SetDefault("Eventide Arrow");    
		}
        */

        private const float bulletFadeTime = 20;


		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
                        Projectile.knockBack = 0.1f;
                        AIType = ProjectileID.WoodenArrowFriendly;
                        Projectile.arrow = true;
         }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);

            if (Projectile.ai[0] == bulletFadeTime && Projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
                Projectile.damage = 2 * Projectile.damage / 2; //nerf damage because 2 shot
                if (Main.myPlayer == Projectile.owner) //spawn extra 1 copies
                {
                    for (int i = 0; i < 1; i++)
                    {//make 2 in total
                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.position.X,
                            Projectile.position.Y,
                            Projectile.velocity.X + inaccuracy(),
                            Projectile.velocity.Y + inaccuracy(),
                            Projectile.type,
                            2 * Projectile.damage / 3,
                            0.2f,
                            Projectile.owner,
                            0,
                            1 // set this to 1 so we don't infinitely spam
                        );
                    }
                }
                Projectile.ai[1] = 1f;
            }  
         if (Projectile.ai[0] < bulletFadeTime) Projectile.ai[0]++;

         Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;      
        }   

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 600);
        }

		public override bool PreDraw(ref Color lightColor)

        {
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) 
            {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		public override void OnKill(int timeLeft)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

                for (int num468 = 0; num468 < 4; num468++)
                {
                  num468 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataADust>(), -Projectile.velocity.X * 0.2f,
                 -Projectile.velocity.Y * 0.2f, 100, default);
                }
	        }
        private static float inaccuracy()
        {
            return Main.rand.NextFloatDirection() * 1.5f;
        }
    }
}
