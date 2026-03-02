
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class DemiseBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {

            int num309 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, DustID.Shadowflame, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.25f);
            Main.dust[num309].velocity *= -0.25f;
            num309 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, DustID.Shadowflame, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.25f);
            Main.dust[num309].velocity *= -0.25f;
            Main.dust[num309].position -= Projectile.velocity * 0.5f;

            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item60, Projectile.position);
            }

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;

            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
                return;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num794 = 4; num794 < 31; num794++)
            {
                float num795 = Projectile.oldVelocity.X * (30f / num794);
                float num796 = Projectile.oldVelocity.Y * (30f / num794);
                int num797 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num795, Projectile.oldPosition.Y - num796), 8, 8, DustID.Shadowflame, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, DustID.Shadowflame, default, 1.8f);
                Main.dust[num797].noGravity = true;
                Main.dust[num797].velocity *= 0.5f;
                num797 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num795, Projectile.oldPosition.Y - num796), 8, 8, DustID.Shadowflame, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, DustID.Shadowflame, default, 1.4f);
                Main.dust[num797].velocity *= 0.05f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, .5f, 1f, 10, false, 0f, 0f, new Color(35, 23, 87));
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, Color.White, false);
            return false;
        }
    }
}