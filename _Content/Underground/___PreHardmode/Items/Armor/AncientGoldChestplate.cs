using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class AncientGoldChestplate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Gold Chainmail");
            // Tooltip.SetDefault(@"You have chance to get gold coins in stoneblocks");
        }

        public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.defense = 4;
            Item.value = 10000;
            Item.expert = true;
            Item.expertOnly = true;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().AncientGoldBody = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ItemID.AncientGoldHelmet && legs.type == ModContent.ItemType<AncientGoldLeggings>();
        }

        public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.AncientGoldSetBonus");
            player.GetModPlayer<AAPlayer>().AncientGoldSet = true;
        }
	}
}
