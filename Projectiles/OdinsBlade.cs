using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    class OdinsBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.scale *= .8f;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] > 7f)
            {
                int num309 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.25f);
                Main.dust[num309].velocity *= -0.25f;
                num309 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.25f);
                Main.dust[num309].velocity *= -0.25f;
                Main.dust[num309].position -= Projectile.velocity * 0.5f;
            }

            AIThrownWeapon(Projectile, ref Projectile.ai, false, 40);

            Projectile.ai[1]++;

            if (Projectile.ai[0] % 5 == 0)
            {
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<AxisSnow>(), Projectile.damage, Projectile.knockBack * 0.55f, Projectile.owner, 0f, Main.rand.Next(3));
                Main.projectile[p].DamageType = DamageClass.Ranged;
                Projectile.netUpdate = true;
            }

            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
                return;
            }
        }

        public static void AIThrownWeapon(Projectile p, ref float[] ai, bool spin = false, int timeUntilDrop = 10, float xScalar = 0.99f, float yIncrement = 0.25f, float maxSpeedY = 16f)
        {
            p.rotation += (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y)) * 0.03f * p.direction;
            ai[0] += 1f;
            if (ai[0] >= timeUntilDrop)
            {
                p.velocity.Y += yIncrement;
                p.velocity.X *= xScalar;
            }
            else
            if (!spin) { p.rotation = (float)Math.Atan2(p.velocity.Y, p.velocity.X) + 2.355f; }
            if (p.velocity.Y > maxSpeedY) { p.velocity.Y = maxSpeedY; }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num794 = 4; num794 < 31; num794++)
            {
                float num795 = Projectile.oldVelocity.X * (30f / num794);
                float num796 = Projectile.oldVelocity.Y * (30f / num794);
                int num797 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num795, Projectile.oldPosition.Y - num796), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 27, default, 1.8f);
                Main.dust[num797].noGravity = true;
                Main.dust[num797].velocity *= 0.5f;
                num797 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num795, Projectile.oldPosition.Y - num796), 8, 8, ModContent.DustType<Dusts.SnowDustLight>(), Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 27, default, 1.4f);
                Main.dust[num797].velocity *= 0.05f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, .5f, 1f, 10, false, 0f, 0f, new Color(35, 23, 87));
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, Color.White, false);
            return false;
        }
    }
}