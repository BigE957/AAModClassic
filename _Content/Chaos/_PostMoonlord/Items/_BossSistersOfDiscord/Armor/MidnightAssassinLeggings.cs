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
	public class MidnightAssassinLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.MidnightAssassin";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Midnight Assassin's Boots");
			/* Tooltip.SetDefault(@"'Dark boots infused with the shadow of midnight'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 300000;
			Item.defense = 20;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Melee) += .15f;
            damageMap.GetDamage(DamageClass.Ranged) += .15f;
            damageMap.GetAttackSpeed(DamageClass.Melee) += .08f;
			AddEffect(new MovementSpeedEffect(0.15f));
			AddEffect(new MaxRunSpeedEffect(0.15f));
        }
    }
}