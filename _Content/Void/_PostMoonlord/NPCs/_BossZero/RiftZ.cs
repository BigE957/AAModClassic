using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero
{
    // to investigate: Projectile.Damage, (8843)
    class RiftZ : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Projectile.rotation += 0.03f;
            if (Projectile.timeLeft > 40)
            {
                if (Projectile.alpha > 30)
                {
                    Projectile.alpha -= 3;
                }
                else
                {
                    Projectile.alpha = 30;
                }
            }
            else
            {
                Projectile.alpha += 3;
            }

            const int aislotHomingCooldown = 0;
            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 10;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[aislotHomingCooldown]++;
            if (Projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                Projectile.ai[aislotHomingCooldown] = homingDelay;

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    Player n = Main.player[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        private int HomeOnTarget()
        {
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                Player n = Main.player[i];
                float distance = Projectile.Distance(n.Center);
                if (distance <= homingMaximumRangeInPixels &&
                    (
                        selectedTarget == -1 || //there is no selected target
                        Projectile.Distance(Main.npc[selectedTarget].Center) > distance)
                )
                    selectedTarget = i;
            }

            return selectedTarget;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D Tex2 = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/_PostMoonlord/NPCs/_BossZero/RiftZ2").Value;
            Texture2D Tex3 = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/_PostMoonlord/NPCs/_BossZero/RiftZ3").Value;
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, Tex.Width, Tex.Height, 0, 0);
            Rectangle frame1 = BaseDrawing.GetFrame(Projectile.frame, Tex2.Width, Tex2.Height, 0, 0);
            Rectangle frame2 = BaseDrawing.GetFrame(Projectile.frame, Tex3.Width, Tex3.Height, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex2, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, -Projectile.rotation, 0, 1, frame1, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex3, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame2, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            float spread = 30f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<RiftSlashZ>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<RiftSlashZ>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
            }
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            Projectile.active = false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.active = false;
            return true;
        }
    }
}
