using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Armor
{
    public class DemonHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<DemonHelmetSetPlayer>().effect = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<DemonHelmetSetEffect_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<DemonHelmetSetEffect_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonHelmetSetEffect_ImpServant>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<DemonHelmetSetEffect_ImpServant>(), 20, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }

    public class DemonHelmetSetPlayer : EquipmentEffectPlayer
    {

    }
}