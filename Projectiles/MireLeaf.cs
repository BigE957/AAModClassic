using AAModClassic.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class MireLeaf : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Leaf");
        }
        public override void SetDefaults()
        {
            Projectile.arrow = true;
            Projectile.width = 34;
            Projectile.height = 22;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            AIType = ProjectileID.WoodenArrowFriendly;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.1f, 0.3f);
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 180f)
            {
                Projectile.ai[0] = 0f;
                Projectile.netUpdate = true;
                int dustIndex = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.GlowingMushroom);
                Main.dust[dustIndex].velocity *= 0.3f;
            }
        }

        public override void OnKill(int i)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            for (int m = 0; m < 12; m++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.BogleafDust>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, Microsoft.Xna.Framework.Color.White, 1.2f);
            }
        }
    }
}

