using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAModClassic._Content.Jungle.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChlorophyteHelmetSummoner : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chlorophyte Face Paint");
			/* Tooltip.SetDefault(@"38% increased minion damage
+80 mana"); */
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;

        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 60000;
			Item.rare = ItemRarityID.Lime;
			Item.defense = 5;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Summon) += .38f;
            player.statManaMax2 += 80;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.ChlorophytePaintBonus");
            player.AddBuff(BuffID.LeafCrystal, 2);
            player.maxMinions += 6;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
			recipe.Register();
		}
	}
}