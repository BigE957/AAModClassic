using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Void.__Hardmode.Items.Tiles.Decoration;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration
{
    public class VoidPreIZBox : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
        
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Sleeping Giant)");

            // Tooltip.SetDefault(@"Plays 'Sleeping Giant' by Cosmoptera");

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
			Item.createTile = ModContent.TileType<VoidPreIZBox_Tile>();
            Item.width = 72;
			Item.height = 36;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.value = 10000;
			Item.accessory = true;
            Item.rare = ItemRarityID.Purple;
        }


        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoidBox>());
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
