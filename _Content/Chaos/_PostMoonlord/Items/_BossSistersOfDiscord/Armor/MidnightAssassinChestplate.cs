using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
	[AutoloadEquip(EquipType.Body)]
	class MidnightAssassinChestplate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Midnight Assassin Shirt");
            /* Tooltip.SetDefault(@"14% increased melee/ranged damage and critical strike chance
20% decreased ammo consumption
+50 Max Life
A dark armor infused with the shadow of midnight"); */
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

        

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 14;
            player.GetCritChance(DamageClass.Ranged) += 14;
            player.GetDamage(DamageClass.Melee) += .14f;
            player.GetDamage(DamageClass.Ranged) += .14f;
            player.statLifeMax2 += 50;
            player.ammoCost80 = true;
        }
	}
}
