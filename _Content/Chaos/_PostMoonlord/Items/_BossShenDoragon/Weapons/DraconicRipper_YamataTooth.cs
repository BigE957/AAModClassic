using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class DraconicRipper_YamataTooth : DraconicRipper_ShenDoragonTooth
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yamata Tooth");
        }

        public override void SetDefaults() // Clones the bullet defaults
        {
            Projectile.CloneDefaults(Terraria.ModLoader.ModContent.ProjectileType<DraconicRipper_ShenDoragonTooth>());
            type = 2;
            Projectile.DamageType = DamageClass.Ranged;
        }
    }
}
