using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Usable
{
    public class Sunpowder : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<Sunpowder>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 4f;
            Item.width = 16;
            Item.height = 24;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noMelee = true;
            Item.value = 75;
        }

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault(@"Cleanses the mire");
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(null, "Hotshroom", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
