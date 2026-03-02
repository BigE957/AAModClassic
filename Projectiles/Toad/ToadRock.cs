using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Toad
{
    public class ToadRock : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            Projectile.penetrate = 1;  
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Rock");
		}

        public override bool PreKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Toadsplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<ShroomDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<ShroomDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            return true;
        }
    }
}
