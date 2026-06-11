using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class MidnightAssassinLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Midnight Assassin's Boots");
			/* Tooltip.SetDefault(@"15% increased ranged/melee damage
15% increased movement speed
8% increased melee speed
Dark boots infused with the shadow of midnight"); */
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
            player.GetDamage(DamageClass.Melee) += .15f;
            player.GetDamage(DamageClass.Ranged) += .15f;
            player.moveSpeed += .15f;
            player.GetAttackSpeed(DamageClass.Melee) += .08f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.15f;
		}
    }
}