using System;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.NPCs._BossSagittarius
{
    public class SagSpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Static Shock");
            Main.projFrames[Projectile.type] = 4;
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

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 130f)
            {
                Projectile.Kill();
            }
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Glitch"), Projectile.Center);
        }
    }
}