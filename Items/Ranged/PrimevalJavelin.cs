using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class PrimevalJavelin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Primeval Javelin");
            // Tooltip.SetDefault("If stuck in an enemy and that enemy dies, releases 4 homing bolts of Dyna-Energy");
        }

        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<PrimevalJavelin>();
            Item.shootSpeed = 12f;
            Item.damage = 70;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.width = 30;
            Item.height = 30;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = 100000;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "DynaskullJavelin");
            recipe.AddIngredient(null, "HeroShards");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
