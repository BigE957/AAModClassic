using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    public class DoomiteHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<DoomiteHelmetSetPlayer>().effect = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<DoomiteHelmetSetEffect_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<DoomiteHelmetSetEffect_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DoomiteHelmetSetEffect_Searcher>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<DoomiteHelmetSetEffect_Searcher>(), 30, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }

    public class DoomiteHelmetSetPlayer : EquipmentEffectPlayer
    {

    }
}