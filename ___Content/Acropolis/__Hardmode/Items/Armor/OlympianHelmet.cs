using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic.Items.Boss.Athena;

namespace AAModClassic.___Content.Acropolis.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class OlympianHelmet : BaseAAItem
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
			return body.type == ModContent.ItemType<OlympianChestplate>() && legs.type == ModContent.ItemType<OlympianLeggings>();
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
            recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}