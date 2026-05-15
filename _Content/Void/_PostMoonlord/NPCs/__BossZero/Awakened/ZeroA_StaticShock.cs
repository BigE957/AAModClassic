using System;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened
{
    public class ZeroA_StaticShock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Static Shock");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = -1;               
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = true;       
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 900;
        }

        int a = 0;

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) a++;
            if (a == 40)
            {
                Projectile.tileCollide = true;
                Projectile.netUpdate = true;
            }
            if (a < 40)
            {
                Projectile.tileCollide = false;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 130f) //projectile time left before disappears
            {
                Projectile.Kill();
            }
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, oldVelocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Glitch"), Projectile.Center);
            return true;
        }
    }
}