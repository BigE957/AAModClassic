using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace AAMod.Items.Armor.Raider
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
			return head.type == Mod.Find<ModItem>("RaiderHelm").Type && legs.type == Mod.Find<ModItem>("RaiderLegs").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.RaiderChestBonus");
            player.noKnockback = true;
            player.endurance += (1 - (player.statLife / player.statLifeMax)) * .1f;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("VikingPlate").Type);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("HydraHide").Type, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("Doomite").Type, 8);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}