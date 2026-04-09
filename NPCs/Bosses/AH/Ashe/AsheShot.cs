using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.AH.Ashe
{
    public class AsheShot : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dayfire");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if(Main.npc[(int)Projectile.ai[0]].type == ModContent.NPCType<AsheRune>())
            {
                if(Projectile.ai[1] ++ < 30)
                {
                    Projectile.alpha += 8;
                    Projectile.scale = 0.8f;
                    Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * .1f;
                }
                else if(Projectile.ai[1] > 60)
                {
                    Projectile.scale = 0.8f;
                    Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 10f;
                }
            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            }
            
            if(Projectile.alpha > 255) Projectile.alpha = 255;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.White.R, Color.White.G, Color.White.B, Projectile.alpha);
        }

        public override void OnKill(int timeLeft)
        {
            if(Projectile.ai[0] == 0 && Projectile.ai[1] == 0) SoundEngine.PlaySound(SoundID.Item124);
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - (spread / 2);
            double deltaAngle = spread / 8f;
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<AsheBoom>(), Projectile.damage, 2);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire_Buff>(), 200);
        }
    }
}