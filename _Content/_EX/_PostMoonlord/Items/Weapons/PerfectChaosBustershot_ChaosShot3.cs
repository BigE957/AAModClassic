using AAModClassic._Content.Chaos.__Hardmode.Items.Weapons;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosBustershot_ChaosShot3 : ChaosBustershot_ChaosShot1
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Shot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            proType = 2;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            offsetLeft = true;
        }
    }
}