using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.BossStandard
{
    public class RajahAwakenedBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            //DisplayName.SetDefault("Music Box (Champion of the Innocent)");
            //Tooltip.SetDefault(@"Plays 'Supreme Justice'");
        }

        

        public override void SetDefaults()
		{
            Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<RajahAwakenedBox_Tile>();
			Item.width = 24;
			Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<RajahBox>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
