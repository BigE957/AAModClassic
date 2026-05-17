using System;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public class ShenDoragon_ChaosFireballFrag : ShenDoragon_ChaosFireballAbstract
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.timeLeft = 30;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 vel = Vector2.Normalize(Projectile.velocity);
                const float ai = 0.01f;
                for (int i = 0; i < 8; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 4);
                    ShenDoragon_ChaosFireballAccel proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<ShenDoragon_ChaosFireballAccel>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), ai).ModProjectile as ShenDoragon_ChaosFireballAccel;
                    proj.Chaos = Chaos;
                    proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<ShenDoragon_ChaosFireballAccel>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), -ai).ModProjectile as ShenDoragon_ChaosFireballAccel;
                    proj.Chaos = Chaos;
                }
            }
        }
    }
}