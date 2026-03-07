using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic;
using AAModClassic.Globals;


namespace AAModClassic.Items.Armor.Dread
{
    [AutoloadEquip(EquipType.Head)]
	public class DreadHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dread Moon Fukumen");
			/* Tooltip.SetDefault(@"24% increased ranged critical chance
20% increased movement speed
+15 Max Life
The abyssal wrath of the Mire rests in this armor"); */

		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;
			Item.value = 3000000;
			Item.defense = 36;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 24;
            player.moveSpeed += .2f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .2f;
            player.statLifeMax2 += 15;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("DreadPlate").Type && legs.type == Mod.Find<ModItem>("DreadBoots").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DreadHelmBonus");

            player.buffImmune[24] = true;
            player.buffImmune[39] = true;
            player.buffImmune[44] = true;
            player.buffImmune[67] = true;
            player.AddBuff(BuffID.Shine, 2);
            player.GetModPlayer<AAPlayer>().dreadSet = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EventideAbyssium", 15);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DepthFukumen", 1);
            recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}