using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    public class FulguriteHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<FulguriteHelmetSetPlayer>().effect = true;
        }
    }

    public class FulguriteHelmetSetPlayer : EquipmentEffectPlayer
    {
        public bool FulguriteRingActive;

        public override void ResetEffects()
        {
            FulguriteRingActive = false;
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (effect)
            {
                if (!FulguriteRingActive)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<FulguriteHelmetSetPlayer_FulguriteRing>(), 40, 6, Main.myPlayer, 0, 0);
                }
            }
        }
    }
}