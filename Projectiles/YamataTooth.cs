namespace AAMod.Projectiles
{
    public class YamataTooth : ShenTooth
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yamata Tooth");
        }

        public override void SetDefaults() // Clones the bullet defaults
        {
            Projectile.CloneDefaults(Terraria.ModLoader.ModContent.ProjectileType<ShenTooth>());
            type = 2;
        }
    }
}
