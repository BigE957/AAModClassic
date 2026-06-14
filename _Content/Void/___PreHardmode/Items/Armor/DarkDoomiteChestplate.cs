using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class DarkDoomiteChestplate : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DarkDoomite";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark Doomite Chestplate");
			// Tooltip.SetDefault(@"Increases minion damage by 6%");

		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 0, 5, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Summon) += 0.06f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 10);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}