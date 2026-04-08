using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Globals;
using AAModClassic.Tiles.Boxes;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class ChaosPreShenBox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sleeping Dragon Music Box");

            // Tooltip.SetDefault(@"Plays 'Sleeping Giants' by LordCakeSpy");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<ChaosPreShenBox_Tile>();
            Item.width = 72;
			Item.height = 36;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            Item.value = 10000;
			Item.accessory = true;
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<InfernoPagodaBox>());
            recipe.AddIngredient(ModContent.ItemType<MireLakeBox>());
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
