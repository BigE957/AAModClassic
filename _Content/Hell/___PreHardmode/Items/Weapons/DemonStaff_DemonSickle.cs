using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Weapons
{
    internal class DemonStaff_DemonSickle : ModProjectile
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
            Projectile.minion = true;
        }

    }
}