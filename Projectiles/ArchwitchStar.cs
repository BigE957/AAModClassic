using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    class ArchwitchStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void AI()
        {
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;

            int dustId = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<InfinityOverloadP>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId].noGravity = true;

            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 30;
            const float amountOfFramesToLerpBy = 30; // minimum of 1, please keep in full numbers even though it's a float!

            Projectile.ai[0]++;
            if (Projectile.ai[0] > homingDelay)
            {
                Projectile.ai[0] = homingDelay; 

                int foundTarget = (int)Projectile.ai[1];
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        public override void OnKill(int timeleft)
        {
            int pieCut = 20;
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<InfinityOverloadP>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<InfinityOverloadP>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(sb, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 3f, 0.9f, 6, true, 0f, 0f, Color.White, default, 9);
            BaseDrawing.DrawTexture(sb, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, default, Projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}