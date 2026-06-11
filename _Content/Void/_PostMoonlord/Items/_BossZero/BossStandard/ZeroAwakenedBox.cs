using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard
{
    public class ZeroAwakenedBox : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Zero Protocol)");
            // Tooltip.SetDefault("Plays 'Doomsday Arrives' by Saucecoie");
        }
        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<ZeroAwakenedBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}

        

        public override void AddRecipes()
        {
            if (Main.expertMode == true)
            { 
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.MusicBox);
                recipe.AddIngredient(ModContent.ItemType<ZeroBox>());
                recipe.AddIngredient(ModContent.ItemType<BrokenCode>());
                recipe.AddTile(TileID.Sawmill);
                recipe.Register();
            }
        }
    }
}
