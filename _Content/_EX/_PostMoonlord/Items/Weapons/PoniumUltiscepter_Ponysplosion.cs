using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PoniumUltiscepter_Ponysplosion : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ponysplosion");
        }

        public override void SetDefaults()
        {
            Projectile.width = 130;
            Projectile.height = 130;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                Projectile.localAI[0] += 1f;
            }
            Projectile.ai[0] += 1f;
            if (Projectile.type == ProjectileID.InfernoFriendlyBlast)
            {
                Projectile.ai[0] += 3f;
            }
            float num461 = 25f;
            if (Projectile.ai[0] > 180f)
            {
                num461 -= (Projectile.ai[0] - 180f) / 2f;
            }
            if (num461 <= 0f)
            {
                num461 = 0f;
                Projectile.Kill();
            }
            if (Projectile.type == ProjectileID.InfernoFriendlyBlast)
            {
                num461 *= 0.7f;
            }
            int num462 = 0;
            while (num462 < num461)
            {
                float num463 = Main.rand.Next(-10, 11);
                float num464 = Main.rand.Next(-10, 11);
                float num465 = Main.rand.Next(3, 9);
                float num466 = (float)System.Math.Sqrt(num463 * num463 + num464 * num464);
                num466 = num465 / num466;
                num463 *= num466;
                num464 *= num466;
                int num467 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, Main.DiscoColor, 1.5f);
                Main.dust[num467].noGravity = true;
                Main.dust[num467].position.X = Projectile.Center.X;
                Main.dust[num467].position.Y = Projectile.Center.Y;
                Dust expr_14B5B_cp_0 = Main.dust[num467];
                expr_14B5B_cp_0.position.X += Main.rand.Next(-10, 11);
                Dust expr_14B85_cp_0 = Main.dust[num467];
                expr_14B85_cp_0.position.Y += Main.rand.Next(-10, 11);
                Main.dust[num467].velocity.X = num463;
                Main.dust[num467].velocity.Y = num464;
                num462++;
            }
        }
    }
}
