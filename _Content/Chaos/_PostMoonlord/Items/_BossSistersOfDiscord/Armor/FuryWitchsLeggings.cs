using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class FuryWitchsLeggings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.FuryWitchs";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fury Witch's Boots");
			/* Tooltip.SetDefault(@"12% increased magic/minion damage
12% increased movement speed
+2 max minions
Boots enchanted with the firey spirit of a supreme dragon acolyte"); */
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
            player.GetDamage(DamageClass.Magic) += .12f;
            player.GetDamage(DamageClass.Summon) += .12f;
            player.moveSpeed += .1f;
            player.maxMinions += 2;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .12f;
		}
        
    }
}