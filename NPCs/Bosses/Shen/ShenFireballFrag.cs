using System;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Shen
{
    public class ShenFireballFrag : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 30;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Discord>(), 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 vel = Vector2.Normalize(Projectile.velocity);
                const float ai = 0.01f;
                for (int i = 0; i < 8; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 4);
                    Projectile.NewProjectile(Projectile.Center, vel, Mod.Find<ModProjectile>("ShenFireballAccel").Type, Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), ai);
                    Projectile.NewProjectile(Projectile.Center, vel, Mod.Find<ModProjectile>("ShenFireballAccel").Type, Projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), -ai);
                }
            }
        }
    }
}