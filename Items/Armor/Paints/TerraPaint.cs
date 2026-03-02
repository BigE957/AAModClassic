using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
	public class TerraPaint : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Face Paint");
			/* Tooltip.SetDefault(@"42% increased minion damage
+120 mana"); */
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


        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = true;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.TerraPaintBonus");
            player.maxMinions += 6;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "HallowedPaint", 1);
            recipe.AddIngredient(null, "ChlorophytePaint", 1);
            recipe.AddTile(TileID.BewitchingTable);
			recipe.Register();
		}
	}
}