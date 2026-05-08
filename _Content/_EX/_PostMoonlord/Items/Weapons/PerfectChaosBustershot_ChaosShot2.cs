using AAModClassic._Content.Chaos.__Hardmode.Items.Weapons;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosBustershot_ChaosShot2 : ChaosBustershot_ChaosShot1
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Shot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            proType = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            offsetLeft = false;
        }
    }
}