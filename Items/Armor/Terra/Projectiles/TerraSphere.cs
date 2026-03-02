using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra.Projectiles
{
    public class TerraSphere : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Bomb");
            Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true; 
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;
			Projectile.ignoreWater = true;
            Projectile.tileCollide = true;          
		}

        public override void AI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.CountsAsClass(DamageClass.Melee))
            {
                int foundTarget = (int)Projectile.ai[1];
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * 30;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / 20);
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeleft)
        {
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int p = Projectile.NewProjectile((int)Projectile.Center.X, (int)Projectile.Center.Y, 0, 0, ModContent.ProjectileType<TerraBoom>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = Projectile.Center;
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Terra, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            
        }
    }
}
