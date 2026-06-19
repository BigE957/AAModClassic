using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class ChaosChestplate : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Chaos";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Chaos Dou");
            // Tooltip.SetDefault(@"7% increased damage");
        }


        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Generic) += .07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:ChaosChestplate");
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}