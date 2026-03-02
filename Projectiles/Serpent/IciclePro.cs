using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAModClassic.Projectiles.Serpent
{
    public class IciclePro : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Blizzard);
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int i = 0; i < 4; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].velocity *= 1.9f;
            }
        }
        public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ice Spike");
            Main.projFrames[Projectile.type] = 1;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 200);
        }
    }
}
