using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class FuryWitchsHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Fury Witch's Cowl");
			/* Tooltip.SetDefault(@"+120 Max Mana
Reduced mana consumption by 20%
+2 Max Minions
10% increased magic/minion damage 
10% increased magic critical strike chance
A hood enchanted with the firey spirit of a supreme dragon acolyte"); */
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
            Item.value = 300000;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.defense = 24;
        }

        

        public override void UpdateEquip(Player player)
        {
            player.manaCost *= .8f;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetDamage(DamageClass.Magic) += .1f;
            player.GetDamage(DamageClass.Summon) += .1f;
            player.maxMinions += 2;
            player.statManaMax2 += 120;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<FuryWitchsChestplate>() && legs.type == ModContent.ItemType<FuryWitchsLeggings>();
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.WitchHoodBonus");
            player.GetDamage(DamageClass.Magic) += .2f;
            player.GetDamage(DamageClass.Summon) += .2f;
            player.maxMinions += 4;

            player.GetModPlayer<AAPlayer>().Witch = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<FuryWitchsHelmet_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<FuryWitchsHelmet_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<FuryWitchsHelmet_FlameSoul>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<FuryWitchsHelmet_FlameSoul>(), 60, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
        
	}
}