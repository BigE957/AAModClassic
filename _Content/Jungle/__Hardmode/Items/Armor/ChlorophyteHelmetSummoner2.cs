using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic._Content.Hallow.__Hardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Jungle.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChlorophyteHelmetSummoner2 : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Face Paint");
			/* Tooltip.SetDefault(@"42% increased minion damage
+120 mana"); */
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 60000;
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 6;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Summon) += .42f;
            player.statManaMax2 += 120;
		}


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.TerraPaintBonus");
            player.maxMinions += 6;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HallowedPaint>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChlorophyteHelmetSummoner>(), 1);
            recipe.AddTile(TileID.BewitchingTable);
			recipe.Register();
		}
	}
}