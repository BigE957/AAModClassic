using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Accessories
{
    public class RajahRabbitsSashOfVengeance : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit's Sash of Vengeance");
            /* Tooltip.SetDefault(@"Every 10% of health lost gives you 8% extra attack power to your highest damage type boost
40% increased movement speed
Increased Jump Height and Speed
Grants Autojump
Immunity to fall damage"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            Color damageColor = Color.Firebrick;
            string DamageType = "";

            if (AAPlayer.MeleeHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAModClassic.Common.RajahSPTooltipMelee");
                damageColor = Color.Firebrick;
            }
            else if (AAPlayer.RangedHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAModClassic.Common.RajahSPTooltipRanged");
                damageColor = Color.SeaGreen;
            }
            else if (AAPlayer.MagicHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAModClassic.Common.RajahSPTooltipMagic");
                damageColor = Color.Violet;
            }
            else if (AAPlayer.SummonHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAModClassic.Common.RajahSPTooltipSummoning");
                damageColor = Color.Cyan;
            }
            else if (AAPlayer.ThrownHighest(player))
            {
                DamageType = Language.GetTextValue("Mods.AAModClassic.Common.RajahSPTooltipThrowing");
                damageColor = Color.DarkOrange;
            }

            string DamageAmount = 100 * DamageBoost(player) + "% ";
            TooltipLine DamageTooltip = new TooltipLine(Mod, "Damage Type", Language.GetTextValue("Mods.AAModClassic.Common.RajahSPDamageBoost") + DamageAmount + DamageType + Language.GetTextValue("Mods.AAModClassic.Common.RajahSPDamageInfo"))
            {
                OverrideColor = damageColor
            };
            tooltips.Add(DamageTooltip);

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            player.autoJump = true;
            Player.jumpHeight = 10;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            player.moveSpeed += .4f;

            if (AAPlayer.MeleeHighest(player))
            {
                player.GetDamage(DamageClass.Melee) += DamageBoost(player);
            }
            else if (AAPlayer.RangedHighest(player))
            {
                player.GetDamage(DamageClass.Ranged) += DamageBoost(player);
            }
            else if (AAPlayer.MagicHighest(player))
            {
                player.GetDamage(DamageClass.Magic) += DamageBoost(player);
            }
            else if (AAPlayer.SummonHighest(player))
            {
                player.GetDamage(DamageClass.Summon) += DamageBoost(player);
            }
            else if (AAPlayer.ThrownHighest(player))
            {
                player.GetDamage(DamageClass.Throwing) += DamageBoost(player);
            }
        }

        public static float DamageBoost(Player player)
        {
            if (player.statLife <= player.statLifeMax * .1f)
            {
                return .72f;
            }
            else if (player.statLife <= player.statLifeMax * .2f)
            {
                return .64f;
            }
            else if (player.statLife <= player.statLifeMax * .3f)
            {
                return .56f;
            }
            else if (player.statLife <= player.statLifeMax * .4f)
            {
                return .48f;
            }
            else if (player.statLife <= player.statLifeMax * .5f)
            {
                return .4f;
            }
            else if (player.statLife <= player.statLifeMax * .6f)
            {
                return .32f;
            }
            else if (player.statLife <= player.statLifeMax * .7f)
            {
                return .24f;
            }
            else if (player.statLife <= player.statLifeMax * .8f)
            {
                return .16f;
            }
            else if (player.statLife <= player.statLifeMax * .9f)
            {
                return .08f;
            }
            else
            {
                return 0f;
            }
        }
    }
}