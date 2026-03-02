using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev.Minions
{
    class FezShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fez");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 900;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;

            int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, ModContent.DustType<InfinityOverloadR>(), Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, ModContent.DustType<InfinityOverloadR>(), Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId3].noGravity = true;
        }

        public override void OnKill(int timeleft)
        {
            Projectile.NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Fezsplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0);
            int pieCut = 20;
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<InfinityOverloadB>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<InfinityOverloadB>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }
}