using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;

namespace AAModClassic.Items.Armor.Witch
{
    [AutoloadEquip(EquipType.Head)]
	public class WitchHood : BaseAAItem
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
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            Item.defense = 24;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
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
			return body.type == Mod.Find<ModItem>("WitchRobe").Type && legs.type == Mod.Find<ModItem>("WitchBoots").Type;
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.WitchHoodBonus");
            player.GetDamage(DamageClass.Magic) += .2f;
            player.GetDamage(DamageClass.Summon) += .2f;
            player.maxMinions += 4;

            player.GetModPlayer<AAPlayer>().Witch = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("FlameSoul").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("FlameSoul").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("FlameSoul").Type] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("FlameSoul").Type, 60, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
        
	}
}