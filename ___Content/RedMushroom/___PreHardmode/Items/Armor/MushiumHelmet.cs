using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Items.Boss.MushroomMonarch;

namespace AAModClassic.___Content.RedMushroom.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class MushiumHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushium Hat");
			// Tooltip.SetDefault("1% Increased life regeneration");

		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 90;
			Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.defense = 3;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 1;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MushiumChestplate>() && legs.type == ModContent.ItemType<MushiumLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.MushiumHatBonus");
            player.pStone = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MushiumBar>(), 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}