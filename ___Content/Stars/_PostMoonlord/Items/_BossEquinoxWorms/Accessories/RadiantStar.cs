using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.___Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class RadiantStar : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Star");
            /* Tooltip.SetDefault(
@"Gives immensely increased stats during the day
'It's Shiny'"); */
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.dayTime)
            {
                player.lifeRegen += 5;
                player.statDefense += 8;
                player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
                player.GetCritChance(DamageClass.Melee) += 4;
                player.GetCritChance(DamageClass.Ranged) += 4;
                player.GetCritChance(DamageClass.Magic) += 4;
                player.pickSpeed -= 0.30f;
                player.GetKnockback(DamageClass.Summon).Base += 0.7f;
                player.GetDamage(DamageClass.Generic) += 0.17f;
                player.GetCritChance(DamageClass.Throwing) += 4;
            }
            player.GetModPlayer<AAPlayer>().RStar = true;
        }

    }
}