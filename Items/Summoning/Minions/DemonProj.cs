using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    internal class DemonProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scythe");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DemonScythe);
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.magic = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Projectile.minion = true;
        }

    }
}