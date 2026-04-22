using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAModClassic.___Content.Jungle.___PreHardmode.Items.Tools
{
    public class Grasscutter : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 5;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 32;
            Item.height = 32;

            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.pick = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 0, 30, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grasscutter");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Vine, 2);
            recipe.AddIngredient(ItemID.Stinger, 2);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
