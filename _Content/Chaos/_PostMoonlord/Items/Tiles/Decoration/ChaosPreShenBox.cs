using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Tiles.Decoration;
using AAModClassic._Content.Mire._PostMoonlord.Items.Tiles.Decoration;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Decoration
{
    public class ChaosPreShenBox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Sleeping Dragon)");

            // Tooltip.SetDefault(@"Plays 'Condemned' by MaestroVGM");
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
