using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class StoneSoldierChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.StoneSoldier";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Soldier Breastplate");
			// Tooltip.SetDefault(@"Increases mining speed by 15%");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 5, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 16;
		}

        public override void RegisterEquipEffects()
        {
            AddEffect(new MiningSpeedEffect(0.15f));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MiningShirt);
            recipe.AddIngredient(ModContent.ItemType<StoneShell>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}