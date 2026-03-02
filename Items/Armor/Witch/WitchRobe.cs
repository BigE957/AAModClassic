using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Witch
{
	[AutoloadEquip(EquipType.Body)]
	class WitchRobe : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fury Witch's Robe");
            /* Tooltip.SetDefault(@"10% increased magic/minion damage 
10% increased critical strike chance
+2 Max Minions
+30 Max Life
A robe enchanted with the firey spirit of a supreme dragon acolyte"); */
        }

        public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 14;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            Item.value = 300000;
            Item.defense = 26;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetDamage(DamageClass.Magic) += .1f;
            player.GetDamage(DamageClass.Summon) += .1f;
            player.maxMinions += 2;
            player.statLifeMax2 += 30;

        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything */
		{
			drawHands = true;
		}
	}
}
