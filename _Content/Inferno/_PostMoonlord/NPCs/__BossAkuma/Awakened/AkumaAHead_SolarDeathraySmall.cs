using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened
{
    public class AkumaAHead_SolarDeathraySmall : AkumaAHead_SolarDeathray
    {
        public override string Texture => ModContent.GetInstance<AkumaAHead_SolarDeathray>().Texture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            maxTime = 60;
            maxScale = 0.2f;
        }

        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<AkumaAHead_SolarDeathray>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0], Projectile.ai[1]);
            base.OnKill(timeLeft);
        }
    }
}