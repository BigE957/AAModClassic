using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
	public class HallowedPaint : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hallowed Face Paint");
			/* Tooltip.SetDefault(@"32% increased minion damage
+100 mana"); */

		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 50000;
			Item.rare = 5;
			Item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Summon) += .32f;
            player.statManaMax2 += 100;
		}

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.HallowedPlateMail && legs.type == ItemID.HallowedGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.HallowedPaintBonus");
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
            player.maxMinions += 6;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
			recipe.Register();
		}
	}
}