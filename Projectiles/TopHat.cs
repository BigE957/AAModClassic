using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TopHat : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;                        //this make the projectile do magic damage
            Projectile.tileCollide = false;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
        }
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            // DisplayName.SetDefault("Top Hat");
        }


        public override void AI()
        {
            //this make that the projectile faces the right way
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            Projectile.alpha = (int)Projectile.localAI[0] * 2;

            if (Projectile.localAI[0] > 600f) //projectile time left before disappears
            {
                Projectile.Kill();
            }

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3) 
                    Projectile.frame = 0; 
            }
            return true;
        }
    }
}