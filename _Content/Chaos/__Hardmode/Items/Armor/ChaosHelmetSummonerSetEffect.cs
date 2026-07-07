using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    public class ChaosHelmetSummonerSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<ChaosHelmetSummonerSetPlayer>().effect = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<ChaosHelmetSummonerSetEffect_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<ChaosHelmetSummonerSetEffect_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<ChaosHelmetSummonerSetEffect_DragonSpirit>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<ChaosHelmetSummonerSetEffect_DragonSpirit>(), 55, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }

    public class ChaosHelmetSummonerSetPlayer : EquipmentEffectPlayer
    {

    }
}