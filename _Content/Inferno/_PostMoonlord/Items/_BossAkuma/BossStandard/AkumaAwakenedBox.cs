using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.BossStandard
{
    public class AkumaAwakenedBox : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Oni Akuma)");
            // Tooltip.SetDefault(@"Plays 'Dawn of the Dragon' by Tyeski");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AkumaAwakenedBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ModContent.RarityType<AncientsRarity>();
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AkumaBox>());
            recipe.AddIngredient(ModContent.ItemType<TaiyangBaolei>());
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
