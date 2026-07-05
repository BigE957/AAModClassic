using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class FuryWitchsLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.FuryWitchs";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fury Witch's Boots");
			/* Tooltip.SetDefault(@"'Boots enchanted with the firey spirit of a supreme dragon acolyte'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 300000;
			Item.defense = 20;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Magic) += .12f;
            damageMap.GetDamage(DamageClass.Summon) += .12f;
			AddEffect(new MovementSpeedEffect(0.10f));
            AddEffect(new MaxMinionSlotEffect(2));
            AddEffect(new MaxRunSpeedEffect(0.12f));
        }
    }
}