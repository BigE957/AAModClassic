using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning.Minions
{
    internal class DevilProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Trident");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.UnholyTridentFriendly);
            Projectile.magic = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Projectile.minion = true;
        }

    }
}