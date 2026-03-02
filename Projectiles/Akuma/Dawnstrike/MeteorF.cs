using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Akuma.Dawnstrike
{
    public class MeteorF : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
        }


        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            int dustType = ModContent.DustType<Dusts.AkumaDust>();
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
            }
            if (Main.rand.Next(3) == 0)
            {
                int dustID2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 2f);
                Main.dust[dustID2].velocity = -Projectile.velocity * 0.5f;
                Main.dust[dustID2].noLight = false;
                Main.dust[dustID2].noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dustType = ModContent.DustType<Dusts.AkumaDust>();
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < 15; m++)
            {
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.Magenta, 1.2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 300);
        }
    }
}