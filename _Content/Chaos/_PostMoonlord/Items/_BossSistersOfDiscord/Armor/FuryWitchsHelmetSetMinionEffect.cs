using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class FuryWitchsHelmetSetMinionEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<FuryWitchsHelmetSetMinionPlayer>().effect = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<FuryWitchsHelmetSetMinionEffect_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<FuryWitchsHelmetSetMinionEffect_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<FuryWitchsHelmetSetMinionEffect_FlameSoul>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<FuryWitchsHelmetSetMinionEffect_FlameSoul>(), 60, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }

    public class FuryWitchsHelmetSetMinionPlayer : EquipmentEffectPlayer
    {

    }
}