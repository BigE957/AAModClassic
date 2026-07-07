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
	class FuryWitchsChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.FuryWitchs";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fury Witch's Robe");
            /* Tooltip.SetDefault(@"'A robe enchanted with the firey spirit of a supreme dragon acolyte'"); */
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }

        public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 14;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.value = 300000;
            Item.defense = 26;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetCritChance(DamageClass.Magic) += 10;
            damageMap.GetDamage(DamageClass.Magic) += .1f;
            damageMap.GetDamage(DamageClass.Summon) += .1f;
            AddEffect(new MaxMinionSlotEffect(2));
            AddEffect(new MaxLifeEffect(30));
        }
    }
}
