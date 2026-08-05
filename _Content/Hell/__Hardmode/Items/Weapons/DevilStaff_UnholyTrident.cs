using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.__Hardmode.Items.Weapons
{
    public class DevilStaff_UnholyTrident : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unholy Trident");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.UnholyTridentFriendly);
            Projectile.minion = true;
        }

    }
}