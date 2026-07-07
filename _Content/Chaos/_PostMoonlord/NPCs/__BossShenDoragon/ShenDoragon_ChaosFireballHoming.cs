using System;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public class ShenDoragon_ChaosFireballHoming : ShenDoragon_ChaosFireballAbstract
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.scale = 4f;
        }

        public override void AI()
        {
            Projectile.velocity = Projectile.DirectionTo(Main.player[(int)Projectile.ai[0]].Center) * Projectile.ai[1];
            if (++Projectile.localAI[0] > 60)
            {
                Projectile.localAI[0] = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vel = Vector2.Normalize(Projectile.velocity);
                    const float ai = 0.015f;
                    for (int i = 0; i < 16; ++i)
                    {
                        vel = vel.RotatedBy(Math.PI / 8);
                        ShenDoragon_ChaosFireballAccel proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<ShenDoragon_ChaosFireballAccel>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f).ModProjectile as ShenDoragon_ChaosFireballAccel;
                        proj.Chaos = Chaos;
                        proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<ShenDoragon_ChaosFireballAccel>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f).ModProjectile as ShenDoragon_ChaosFireballAccel;
                        proj.Chaos = Chaos;
                    }
                }
            }
            Projectile.scale -= 3f / 300f;
            if (Projectile.scale <= 1)
                Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 vel = Vector2.Normalize(Projectile.velocity);
                const float ai = 0.015f;
                for (int i = 0; i < 16; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 8);
                    ShenDoragon_ChaosFireballAccel proj = Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<ShenDoragon_ChaosFireballAccel>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f).ModProjectile as ShenDoragon_ChaosFireballAccel;
                    proj.Chaos = Chaos;
                    proj = Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, vel, ModContent.ProjectileType<ShenDoragon_ChaosFireballAccel>(), Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f).ModProjectile as ShenDoragon_ChaosFireballAccel;
                    proj.Chaos = Chaos;
                }
            }
        }
    }
}