using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Mushium
{
    [AutoloadEquip(EquipType.Head)]
	public class MushiumHat : BaseAAItem
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
			return body.type == Mod.Find<ModItem>("MushiumShirt").Type && legs.type == Mod.Find<ModItem>("MushiumPants").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.MushiumHatBonus");
            player.pStone = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}