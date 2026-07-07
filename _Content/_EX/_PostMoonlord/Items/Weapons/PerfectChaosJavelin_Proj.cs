using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosJavelin_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perfect Chaos Javelin");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int Proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<PerfectChaosJavelin_PerfectChaosBlast>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, Main.rand.Next(2), 1);
            Main.projectile[Proj].Center = Projectile.Center;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= 45f)
                {
                    float num975 = 0.98f;
                    float num976 = 0.35f;
                    Projectile.ai[1] = 45f;
                    Projectile.velocity.X = Projectile.velocity.X * num975;
                    Projectile.velocity.Y = Projectile.velocity.Y + num976;
                }
                Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            }
        }
    }
}
