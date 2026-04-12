using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

using System;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.___Content.Acropolis.Projectiles
{
    public class SeraphFeather : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 1f, 1f, 5, false, 0f, 0f, lightColor);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, lightColor, false);
            return false;
        }
    }
}