using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
	[AutoloadEquip(EquipType.Body)]
	class MidnightAssassinChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.MidnightAssassin";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Midnight Assassin Shirt");
            /* Tooltip.SetDefault(@"'A dark armor infused with the shadow of midnight'"); */
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }

        public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 14;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.value = 300000;
            Item.defense = 29;
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += .14f;
            damageMap.GetDamage(DamageClass.Ranged) += .14f;
            damageMap.GetCritChance(DamageClass.Melee) += 14;
            damageMap.GetCritChance(DamageClass.Ranged) += 14;
            AddEffect(new MaxLifeEffect(50));
            AddEffect<AmmoCost80Effect>();
        }
    }
}
