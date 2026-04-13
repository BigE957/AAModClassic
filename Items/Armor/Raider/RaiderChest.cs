using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic.Items.Armor.Viking;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;

namespace AAModClassic.Items.Armor.Raider
{
    [AutoloadEquip(EquipType.Body)]
	public class RaiderChest : BaseAAItem
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Raider Chestplate");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 0, 5, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 14;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ModContent.ItemType<RaiderHelm>() && legs.type == ModContent.ItemType<RaiderLegs>();
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.RaiderChestBonus");
            player.noKnockback = true;
            player.endurance += (1 - (player.statLife / player.statLifeMax)) * .1f;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<VikingPlate>());
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.DoomiteBar>(), 8);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}