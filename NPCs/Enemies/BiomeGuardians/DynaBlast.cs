using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.BiomeGuardians
{
    public class DynaBlast : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Bomb");     
            Main.projFrames[Projectile.type] = 4;     
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            
		}

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.width, Projectile.height), Projectile.width, Projectile.height, DustID.Torch, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("DynaBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
