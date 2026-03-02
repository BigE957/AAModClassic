using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.GlowingMushium
{
    [AutoloadEquip(EquipType.Head)]
	public class ShroomHat : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glowing Mushium Hat");
            // Tooltip.SetDefault("2% increased mana regeneration");

		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 90;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 2;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

		public override void UpdateEquip(Player player)
        {
            player.manaRegenBonus += 2;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("ShroomShirt").Type && legs.type == Mod.Find<ModItem>("ShroomPants").Type;
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ShroomHatBonus");

            player.buffImmune[BuffID.ManaSickness] = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "GlowingMushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
