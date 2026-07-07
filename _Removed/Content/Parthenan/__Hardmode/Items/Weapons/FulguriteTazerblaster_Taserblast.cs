using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Weapons
{
    public class FulguriteTazerblaster_Taserblast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Taserblast");
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 3;                       //this is the projectile penetration
            Main.projFrames[Projectile.type] = 4;           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 900;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 10;
            }
            else
            {
                Projectile.alpha = 0;
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 130f) //projectile time left before disappears
            {
                Projectile.Kill();
            }
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 1;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, oldVelocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }
    }
}