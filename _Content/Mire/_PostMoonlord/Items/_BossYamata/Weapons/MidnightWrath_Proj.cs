using AAModClassic.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class MidnightWrath_Proj : Javelin
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Midnight Wrath");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.GetGlobalProjectile<ImplaingProjectile>().CanImpale = true;
            Projectile.GetGlobalProjectile<ImplaingProjectile>().damagePerImpaler = 24;
            maxStickingJavelins = 12;
            rotationOffset = (float)Math.PI / 4;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.velocity.X > 0 ? Projectile.rotation : Projectile.rotation - (float)Math.PI/2, tex.Size() / 2f, Projectile.scale, Projectile.velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 46, default, 1.381579f);
            }
        }
    }
}