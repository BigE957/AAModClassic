using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.BossStandard
{
	public class SagittariusBox : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
        
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Sagittarius)");

            // Tooltip.SetDefault(@"Plays 'Event Horizon' by SpectralAves");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<SagittariusBox_Tile>();
            Item.width = 72;
			Item.height = 36;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 5);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
