using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Assassin
{
    [AutoloadEquip(EquipType.Head)]
	public class AssassinHood : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Midnight Assassin Hood");
			/* Tooltip.SetDefault(@"13% increased melee/ranged damage and critical strike chance
A dark hood infused with the shadow of midnight"); */
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
            Item.value = 300000;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            Item.defense = 25;
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
            player.GetCritChance(DamageClass.Melee) += 13;
            player.GetCritChance(DamageClass.Ranged) += 13;
            player.GetDamage(DamageClass.Melee) += .13f;
            player.GetDamage(DamageClass.Ranged) += .13f;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("AssassinShirt").Type && legs.type == Mod.Find<ModItem>("AssassinBoots").Type;
        }

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.AssassinBonus");
            //Double tap down to go into stealth mode
            //Movement is not impeded while in stealth mode
            //Melee and Ranged damage increased while in stealth";
            player.GetAttackSpeed(DamageClass.Melee) += .3f;
            
            //player.dash = 2;
            //player.aggro -= 6;
            //player.rangedDamage += .2f;
            //player.meleeDamage += .2f;
            player.GetModPlayer<AAPlayer>().Assassin = true;
        }
	}
}