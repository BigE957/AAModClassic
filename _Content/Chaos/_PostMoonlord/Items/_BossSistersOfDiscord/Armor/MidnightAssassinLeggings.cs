using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class MidnightAssassinLeggings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.MidnightAssassin";
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
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
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