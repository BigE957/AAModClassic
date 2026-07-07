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