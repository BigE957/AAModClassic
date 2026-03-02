using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    internal class TruePrismshot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Prism Shot");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 100;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }

        public override bool PreDraw(ref Color lightColor)
        {

            return false;
        }

        public override void AI()
        {
            for (int num443 = 0; num443 < 2; num443++)
            {
                Vector2 vector31 = Projectile.position;
                vector31 -= Projectile.velocity * (num443 * 0.25f);
                Projectile.alpha = 255;
                int num444 = Dust.NewDust(vector31, 1, 1, ModContent.DustType<Dusts.HallowedDustT>(), 0f, 0f, 0);
                Main.dust[num444].noGravity = true;
                Main.dust[num444].position = vector31;
                Dust expr_13D2C_cp_0 = Main.dust[num444];
                expr_13D2C_cp_0.position.X += Projectile.width / 2;
                Dust expr_13D50_cp_0 = Main.dust[num444];
                expr_13D50_cp_0.position.Y += Projectile.height / 2;
                Main.dust[num444].color = Main.DiscoColor;
                Main.dust[num444].scale = Main.rand.Next(70, 110) * 0.05f;
                Main.dust[num444].velocity *= 0.2f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num585 = 0; num585 < 20; num585++)
            {
                int num586 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 50, Main.DiscoColor);
                Main.dust[num586].noGravity = true;
                Main.dust[num586].velocity *= 4f;
            }
        }
    }
}