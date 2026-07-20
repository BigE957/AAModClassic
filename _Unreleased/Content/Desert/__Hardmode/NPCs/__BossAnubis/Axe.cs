using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using System;
using AAModClassic.UI.World;

namespace AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    public class Axe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Axe");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? 1 : 0;
        }

        public override void AI()
        {
            if (Projectile.velocity.X < 0)
                Projectile.direction = -1;
            else
                Projectile.direction = 1;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Projectile.velocity.Y += 0.2f;
                Projectile.rotation += .15f * Projectile.velocity.X * Projectile.direction;
            }
            else
            {
                Projectile.ai[0]++;
                if (Projectile.ai[0] >= 15f || Projectile.Center.X <= Projectile.ai[1] - 20 && Projectile.Center.X <= Projectile.ai[1] + 20)
                {
                    Projectile.ai[0] = 15f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
                    Projectile.velocity.X *= .94f;
                }
                if (Projectile.velocity.Y > 16f)
                {
                    Projectile.velocity.Y = 16f;
                }

                Projectile.rotation += .3f * Math.Abs(Projectile.velocity.X) * Projectile.direction;
            }


        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height(), 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, -Projectile.direction, 1, frame, lightColor, true);
            return false;
        }
    }
}
