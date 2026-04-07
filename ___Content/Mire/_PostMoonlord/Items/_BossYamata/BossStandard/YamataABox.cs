using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.Accessories;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.BossStandard
{
    public class YamataABox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Yamata Awakened Music Box");
            // Tooltip.SetDefault(@"Plays 'Abyssal Nightmare' by Universe");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.YamataA;
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
			Item.createTile = ModContent.TileType<YamataABox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<YamataBox>());
            recipe.AddIngredient(ModContent.ItemType<Naitokurosu>());
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
