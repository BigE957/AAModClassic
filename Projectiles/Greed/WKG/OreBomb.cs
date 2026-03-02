using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Greed.WKG
{
    public class OreBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            Projectile.penetrate = 1;  
            Projectile.width = 44;
            Projectile.height = 44;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ore Cluster");
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }

        public override void OnKill(int a)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < Main.rand.Next(5, 10); i++)
            {
                int x = Main.rand.Next(-6, 6);
                int y = -Main.rand.Next(3, 5);
                int p = Projectile.NewProjectile(Projectile.position, new Vector2(x, y), ModContent.ProjectileType<OreChunkM>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0, Main.rand.Next(23));
                Main.projectile[p].Center = Projectile.Center - new Vector2(0, 25);

                if (Main.projectile[p].ai[1] == 10)
                {
                    Main.projectile[p].knockBack *= 1.5f;
                }
                if (Main.projectile[p].ai[1] == 19)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Vector2 perturbedSpeed = new Vector2(x, y).RotatedByRandom(MathHelper.ToRadians(20));
                        int q = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<OreChunkM>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 5, 19);
                        Main.projectile[q].Center = Projectile.Center - new Vector2(0, 4);
                    }
                }
            }
        }
    }
}
