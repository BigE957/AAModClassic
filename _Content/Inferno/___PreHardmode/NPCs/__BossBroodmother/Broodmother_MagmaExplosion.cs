using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother
{
    public class Broodmother_MagmaExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magma Explosion");
			Main.projFrames[Projectile.type] = 4;
        }
		
        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 100;
        }

		bool playedSound = false;
        public override void AI()
        {
			if(!playedSound)
			{
				playedSound = true;
				SoundEngine.PlaySound(SoundID.Item88, Projectile.Center);				
			}
			Projectile.velocity = Vector2.Zero;
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame > 3)
                {
					Projectile.frame = 3;
                    if(Main.netMode != NetmodeID.MultiplayerClient) 
						Projectile.Kill();
                }
            }			
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}		
    }
}