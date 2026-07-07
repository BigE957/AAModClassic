using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    public class Anubis_Runeblast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.extraUpdates = 1;
        }


        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            int dustType = ModContent.DustType<Dusts.JudgementDust>();
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
            }
            if (Main.rand.NextBool(3))
            {
                int dustID2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID2].velocity = -Projectile.velocity * 0.5f;
                Main.dust[dustID2].noLight = false;
                Main.dust[dustID2].noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dustType = ModContent.DustType<Dusts.JudgementDust>();
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}