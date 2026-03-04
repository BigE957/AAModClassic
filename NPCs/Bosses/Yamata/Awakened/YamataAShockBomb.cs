using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Yamata.Awakened
{
    public class YamataAShockBomb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Storm");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 90;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.alpha = 0;
            CooldownSlot = 1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 250, 250, 0);
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.9f / 255f, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * 0.4f / 255f);
        	Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
            Projectile.velocity.Y += 0.2f;
            if (Projectile.ai[0] > -1 && Projectile.ai[0] < 255 && Projectile.Center.Y > Main.player[(int)Projectile.ai[0]].Center.Y)
                Projectile.Kill();
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HydraToxin").Type, 300);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item89);
	    	if (Main.netMode != NetmodeID.MultiplayerClient)
	    	{
                const float ai0 = 20;
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModProjectile>("Shockwave2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, ai0);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModProjectile>("Shockwave2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, -ai0);
            }
        	for (int dust = 0; dust <= 10; dust++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.LifeDrain, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}