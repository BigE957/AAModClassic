using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Equinox
{
    public class Moonray : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Moonray");
		}

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.scale = 2f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
			Projectile.extraUpdates = 2;
			Projectile.timeLeft = 1000;
            CooldownSlot = 1;
        }

		public bool playedSound = false;		
        public override void AI()
        {
			if(!playedSound)
			{
				playedSound = true;
				SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);				
			}
			Effects();
			if(Projectile.velocity.Length() < 12f)
			{
				Projectile.velocity.X *= 1.05f;
				Projectile.velocity.Y *= 1.05f;
			}
        	Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        }

		public virtual void Effects()
		{
	        Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.05f / 255f, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0.5f / 255f);		
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
        }

        public int dontDrawDelay = 2;

        public override bool PreDraw(ref Color lightColor)
        {
			dontDrawDelay = Math.Max(0, dontDrawDelay - 1);
			return dontDrawDelay == 0;
		}
    }
}