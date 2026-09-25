using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAModClassic._Content.Hoard.__Hardmode.Items.Armor
{
    public class StoneSoldierHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StoneSoldierHelmetSetPlayer>().effect = true;
        }
    }

    public class StoneSoldierHelmetSetPlayer : EquipmentEffectPlayer
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect)
            {
                if (target.life <= 0 && Main.rand.NextBool(80))
                {
                    Projectile.NewProjectile(target.GetSource_GiftOrReward(), target.Center, Vector2.Zero, ProjectileID.CoinPortal, 0, 0, Main.myPlayer);
                }
            }
        }
    }
}