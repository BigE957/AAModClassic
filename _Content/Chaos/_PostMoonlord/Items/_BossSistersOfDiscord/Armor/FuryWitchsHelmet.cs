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
	public class FuryWitchsHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.FuryWitchs";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Fury Witch's Cowl");
			/* Tooltip.SetDefault(@"'A hood enchanted with the firey spirit of a supreme dragon acolyte'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
            Item.value = 300000;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.defense = 24;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<FuryWitchsChestplate>() && legs.type == ModContent.ItemType<FuryWitchsLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Magic) += .1f;
            damageMap.GetCritChance(DamageClass.Magic) += 10;
            damageMap.GetDamage(DamageClass.Summon) += .1f;
            AddEffect(new ManaCostMultiplierEffect(0.80f));
            AddEffect(new MaxMinionSlotEffect(2));
            AddEffect(new MaxManaEffect(120));

            setDamageMap.GetDamage(DamageClass.Magic) += .2f;
            setDamageMap.GetDamage(DamageClass.Summon) += .2f;
            AddSetEffect(new MaxMinionSlotEffect(4));
            AddSetEffect<FuryWitchsHelmetSetMinionEffect>();
            AddSetEffect<FuryWitchsHelmetSetHotkeyEffect>();
            AddSetEffect<FuryWitchsHelmetSetDescEffect>();
        }
    }

    public class FuryWitchsHelmetSetDescEffect : EquipmentEffectData
    {

    }
}