using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning.Minions
{
    public class SkullShot : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Skull Shot");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.minion = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 6f)
            {
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                for (int num151 = 0; num151 < 40; num151++)
                {
                    int num152 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, 0f, 0f, 100);
                    Main.dust[num152].velocity *= 3f;
                    Main.dust[num152].velocity += Projectile.velocity * 0.75f;
                    Main.dust[num152].scale *= 1.2f;
                    Main.dust[num152].noGravity = true;
                }
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 6f)
            {
                for (int num153 = 0; num153 < 3; num153++)
                {
                    int num154 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                    Main.dust[num154].velocity *= 0.6f;
                    Main.dust[num154].scale *= 1.4f;
                    Main.dust[num154].noGravity = true;
                }
            }
        }
    }
}