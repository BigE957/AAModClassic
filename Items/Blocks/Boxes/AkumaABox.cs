using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class AkumaABox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Akuma Awakened Music Box");
            // Tooltip.SetDefault(@"Plays 'Dawn of the Dragon' by Universe");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("AkumaABox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AkumaBox");
            recipe.AddIngredient(null, "TaiyangBaolei");
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
