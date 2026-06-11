using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class RaiderLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Raider Greaves");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 12;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VikingLeggings>());
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 6);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}