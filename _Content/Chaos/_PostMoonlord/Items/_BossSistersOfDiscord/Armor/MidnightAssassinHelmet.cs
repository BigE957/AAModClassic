using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class MidnightAssassinHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.MidnightAssassin";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Midnight Assassin Hood");
			/* Tooltip.SetDefault(@"'A dark hood infused with the shadow of midnight'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
            Item.value = 300000;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.defense = 25;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MidnightAssassinChestplate>() && legs.type == ModContent.ItemType<MidnightAssassinLeggings>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += .13f;
            damageMap.GetDamage(DamageClass.Ranged) += .13f;
            damageMap.GetCritChance(DamageClass.Melee) += 13;
            damageMap.GetCritChance(DamageClass.Ranged) += 13;

            setDamageMap.GetAttackSpeed(DamageClass.Melee) += .3f;
            AddSetEffect<MidnightAssassinHelmetSetEffect>();
            AddSetEffect<MidnightAssassinHelmetSetDescEffect>();
        }
    }

    public class MidnightAssassinHelmetSetDescEffect : EquipmentEffectData
    {

    }
}