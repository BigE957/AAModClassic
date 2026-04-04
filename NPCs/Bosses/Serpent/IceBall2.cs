using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Serpent
{
    public class IceBall2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Ball");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            Projectile.penetrate = 1;  
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 300;
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

        public override bool PreAI()
        {
            Projectile.frame = (int)Projectile.ai[1];
            return true;
        }

        public override bool PreKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.IceDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / pieCut * 6.28f);
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.IceDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m /pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
            }
            return true;
        }
    }
}
