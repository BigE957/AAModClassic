using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic.Items.Armor.Olympian
{
    [AutoloadEquip(EquipType.Head)]
	public class OlympianHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Olympian Helmet");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
            Item.defense = 8;
        }
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("OlympianPlate").Type && legs.type == Mod.Find<ModItem>("OlympianBoots").Type;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.OlympianHelmBonus");

			player.GetCritChance(DamageClass.Melee) += 60;
			player.GetCritChance(DamageClass.Ranged) += 60;
			player.GetCritChance(DamageClass.Magic) += 60;
			player.GetCritChance(DamageClass.Throwing) += 60;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GladiatorHelmet);
            recipe.AddIngredient(null, "GoddessFeather", 6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}