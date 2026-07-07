using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.PumpkinMoon.__Hardmode.Items.Weapons
{
    public class SpookySawblade_Proj : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<SpookySawblade>().Texture;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PossessedHatchet);
            Projectile.penetrate = 6;  
            Projectile.width = 32;
            Projectile.height = 32;
            AIType = ProjectileID.PossessedHatchet;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SpookerangP");
        }


    }
}
