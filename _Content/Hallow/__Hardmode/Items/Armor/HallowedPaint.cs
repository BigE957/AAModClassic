using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Hallow.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class HallowedPaint : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Hallowed";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hallowed Face Paint");
			/* Tooltip.SetDefault(@"32% increased minion damage
+100 mana"); */
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Summon) += .32f;
            player.statManaMax2 += 100;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.HallowedPlateMail && legs.type == ItemID.HallowedGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.HallowedPaintBonus");
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