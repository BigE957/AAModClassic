using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class AncientGoldChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.AncientGold";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Gold Chainmail");
        }

        public override void SetDefaults()
		{
			Item.width = 18;
            Item.height = 18;
            Item.defense = 4;
            Item.value = 10000;
            Item.expert = true;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ItemID.AncientGoldHelmet && legs.type == ModContent.ItemType<AncientGoldLeggings>();
        }

        public override void RegisterEquipStats()
        {
            AddEffect<AncientGoldChestplateEffect>();

            AddSetEffect<AncientGoldChestplateSetEffect>();
        }
    }
}
