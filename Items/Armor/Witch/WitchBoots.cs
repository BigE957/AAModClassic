using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Witch
{
    [AutoloadEquip(EquipType.Legs)]
	public class WitchBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fury Witch's Boots");
			/* Tooltip.SetDefault(@"12% increased magic/minion damage
12% increased movement speed
+2 max minions
Boots enchanted with the firey spirit of a supreme dragon acolyte"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 300000;
			Item.defense = 20;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
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
            player.GetDamage(DamageClass.Magic) += .12f;
            player.GetDamage(DamageClass.Summon) += .12f;
            player.moveSpeed += .1f;
            player.maxMinions += 2;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .12f;
		}
        
    }
}