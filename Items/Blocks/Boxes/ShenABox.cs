using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Tiles.Boxes;
using AAModClassic.Items.Boss.Shen;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class ShenABox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shen Doragon Awakened Music Box");
            
            // Tooltip.SetDefault(@"Plays 'Blaze of Glory' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<ShenABox_Tile>();
            Item.width = 72;
			Item.height = 36;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<ShenBox>());
            recipe.AddIngredient(ModContent.ItemType<ChaosSoul>());
            recipe.AddTile(TileID.Sawmill);
            recipe.AddCondition(Condition.InExpertMode);
            recipe.Register();
        }
    }
}
