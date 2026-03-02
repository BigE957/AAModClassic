using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Projectiles
{
    public class ChaosShot1 : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public int proType = 0;
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("DNA");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
            Projectile.alpha = 255;
            Projectile.tileCollide = true;
        }

        public float vectorOffset = 0f;
        public bool offsetLeft = false;
        public Vector2 originalVelocity = Vector2.Zero;

        public override void AI()
        {
            int dustType = proType == 0 ? ModContent.DustType<Dusts.DiscordLight>() : proType == 1 ? ModContent.DustType<Dusts.AkumaDustLight>() : ModContent.DustType<Dusts.YamataDustLight>();
            if (Projectile.ai[1] != 0)
            {
                Projectile.extraUpdates = 1;
                Projectile.usesLocalNPCImmunity = true;
                Projectile.localNPCHitCooldown = 5;
            }
            else
            {
                Projectile.penetrate = 1;
            }

            int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1) - Projectile.velocity, 2, 2, dustType, 0f, 0f, 100, Color.White, 1.2f);
            Main.dust[dustID].velocity *= 0f;
            Main.dust[dustID].noLight = false;
            Main.dust[dustID].noGravity = true;

            if (originalVelocity == Vector2.Zero)
            {
                originalVelocity = Projectile.velocity;
            }
            if (proType != 0)
            {
                if (offsetLeft)
                {
                    vectorOffset -= 0.08f;
                    if (vectorOffset <= -0.5f)
                    {
                        vectorOffset = -0.5f;
                        offsetLeft = false;
                    }
                }
                else
                {
                    vectorOffset += 0.08f;
                    if (vectorOffset >= 0.5f)
                    {
                        vectorOffset = 0.5f;
                        offsetLeft = true;
                    }
                }
                float velRot = BaseUtility.RotationTo(Projectile.Center, Projectile.Center + originalVelocity);
                Projectile.velocity = BaseUtility.RotateVector(default, new Vector2(Projectile.velocity.Length(), 0f), velRot + (vectorOffset * 0.5f));
            }
            Projectile.rotation = BaseUtility.RotationTo(Projectile.Center, Projectile.Center + Projectile.velocity) + 1.57f - MathHelper.PiOver4;
            Projectile.spriteDirection = 1;
        }

        public override void OnKill(int timeLeft)
        {
            int dustType = proType == 0 ? 0 : proType == 1 ? ModContent.DustType<Dusts.AkumaDustLight>() : ModContent.DustType<Dusts.YamataAuraDust>();
            if (proType != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, 0f, 0f, 100, default, 1.2f);
                    Main.dust[dustIndex].velocity *= 1.9f;
                }
            }
        }
    }
}