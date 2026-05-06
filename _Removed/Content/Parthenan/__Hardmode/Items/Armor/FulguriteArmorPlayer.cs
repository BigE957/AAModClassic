using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using Terraria.ID;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    public class FulguriteArmorPlayer : ModPlayer
    {
        public bool FulguriteArmorSetBonus;
        public bool FulguriteRingActive;

        public override void ResetEffects()
        {
            FulguriteArmorSetBonus = false;
            FulguriteRingActive = false;
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (FulguriteArmorSetBonus)
            {
                if (!FulguriteRingActive)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<FulguriteArmorPlayer_FulguriteRing>(), 40, 6, Main.myPlayer, 0, 0);
                }
            }
        }
    }
}