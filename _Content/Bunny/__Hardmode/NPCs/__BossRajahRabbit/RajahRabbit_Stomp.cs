using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit
{
    public class RajahRabbit_Stomp: ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stomp");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 30;
        }
    }
}
