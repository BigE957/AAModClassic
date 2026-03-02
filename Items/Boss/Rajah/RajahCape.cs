using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Rajah
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class RajahCape : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit's Cloak of Supremecy");
            /* Tooltip.SetDefault(@"Every 10% of health lost gives you:
1. 12% extra attack power to your highest damage type boost
2. 5% increased movement speed
All effects of the Sash of Vengeance
'You have been deemed a worthy successor by the Champion of the Innocent'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 9;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
            Item.defense = 10;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            Color damageColor = Color.Firebrick;
            string DamageType = "";

            if (modPlayer.MeleeHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipMelee");
                damageColor = Color.Firebrick;
            }
            else if (modPlayer.RangedHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipRanged");
                damageColor = Color.SeaGreen;
            }
            else if (modPlayer.MagicHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipMagic");
                damageColor = Color.Violet;
            }
            else if (modPlayer.SummonHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipSummoning");
                damageColor = Color.Cyan;
            }
            else if (modPlayer.ThrownHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAMod.Common.RajahSPTooltipThrowing");
                damageColor = Color.DarkOrange;
            }

            string DamageAmount = (100 * DamageBoost(player)) + "% ";
            TooltipLine DamageToltip = new TooltipLine(Mod, "Damage Type", Language.GetTextValue("Mods.AAMod.Common.RajahSPDamageBoost") + DamageAmount + DamageType + Language.GetTextValue("Mods.AAMod.Common.RajahSPDamageInfo"))
            {
                OverrideColor = damageColor
            };
            tooltips.Add(DamageToltip);

            string SpeedAmount = (100 * Speed(player)) + "% ";
            TooltipLine SpeedTooltip = new TooltipLine(Mod, "Damage Type", Language.GetTextValue("Mods.AAMod.Common.RajahSPSpeedBoost") + SpeedAmount);
            tooltips.Add(SpeedTooltip);

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            player.autoJump = true;
            Player.jumpHeight = 40;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            player.moveSpeed += Speed(player);
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += Speed(player);

            if (modPlayer.MeleeHighest(player))
            {
                player.GetDamage(DamageClass.Melee) += DamageBoost(player);
            }
            else if (modPlayer.RangedHighest(player))
            {
                player.GetDamage(DamageClass.Ranged) += DamageBoost(player);
            }
            else if (modPlayer.MagicHighest(player))
            {
                player.GetDamage(DamageClass.Magic) += DamageBoost(player);
            }
            else if (modPlayer.SummonHighest(player))
            {
                player.GetDamage(DamageClass.Summon) += DamageBoost(player);
            }
            else if (modPlayer.ThrownHighest(player))
            {
                player.GetDamage(DamageClass.Throwing) += DamageBoost(player);
            }
        }

        public float DamageBoost(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return 1.08f;
            }
            else if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .96f;
            }
            else if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .84f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .72f;
            }
            else if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .60f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .48f;
            }
            else if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .36f;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .24f;
            }
            else if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .12f;
            }

            return 0f;
        }

        public float Speed(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return .45f;
            }
            else if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .4f;
            }
            else if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .35f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .3f;
            }
            else if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .25f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .2f;
            }
            else if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .15f;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .1f;
            }
            else if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .05f;
            }

            return 0f;
        }
    }
    
}