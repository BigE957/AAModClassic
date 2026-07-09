using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class DraconicRipper_AkumaTooth : DraconicRipper_ShenDoragonTooth
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Akuma Tooth");
        }

        public override void SetDefaults() // Clones the bullet defaults
        {
            Projectile.CloneDefaults(Terraria.ModLoader.ModContent.ProjectileType<DraconicRipper_ShenDoragonTooth>());
            type = 1;
            Projectile.DamageType = DamageClass.Ranged;
        }
    }
}
