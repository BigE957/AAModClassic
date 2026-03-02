using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class Mushmallet : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 30;
            Item.useTime = 22;
            Item.useAnimation = 30;
            Item.hammer = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushmallet");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
