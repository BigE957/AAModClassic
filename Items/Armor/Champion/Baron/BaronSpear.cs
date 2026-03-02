using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using AAMod.Projectiles;
using Terraria.GameContent;

namespace AAMod.Items.Armor.Champion.Baron
{
    public class BaronSpear : Javelin
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baron's Spear");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = 1;
            Projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().CanImpale = true;
            Projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().damagePerImpaler = 150;
            maxStickingJavelins = 15;
            rotationOffset = (float)Math.PI / 4;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            spriteBatch.Draw(texture, new Vector2(Projectile.Center.X - Main.screenPosition.X, Projectile.Center.Y - Main.screenPosition.Y + 2),
                        new Rectangle(0, 0, texture.Width, texture.Height), Color.White, Projectile.rotation,
                        new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}
