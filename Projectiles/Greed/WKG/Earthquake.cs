using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Greed.WKG
{
    public class Earthquake : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 5;
        }

        public override void AI()
        {
            Vector2 bottom = Projectile.Bottom;
            for (float num3 = 0f; num3 < 20; num3++)
            {
                Dust dust3 = Dust.NewDustDirect(Projectile.Bottom, Projectile.width, 1, DustID.Stone, 0f, 0f, 0, default, 1f);
                dust3.alpha = 0;
                dust3.position.Y = bottom.Y;
                Dust expr_336_cp_0 = dust3;
                expr_336_cp_0.velocity.Y -= 3f;
                Dust expr_34E_cp_0 = dust3;
                expr_34E_cp_0.velocity.X *= 0.5f;
                dust3.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
            }
            for (float num4 = 0f; num4 < 20; num4++)
            {
                Dust dust4 = Dust.NewDustDirect(Projectile.Bottom, Projectile.width, 1, DustID.Dirt, 0f, 0f, 0, default, 1f);
                dust4.position.Y = bottom.Y;
                Dust expr_433_cp_0 = dust4;
                expr_433_cp_0.velocity.Y -= 5f;
                Dust expr_44B_cp_0 = dust4;
                expr_44B_cp_0.velocity.X *= 0.8f;
                dust4.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.velocity.Y = knockback * target.knockBackResist;
        }
    }
}