using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Projectiles.GemShot
{
    public class PrismBolt : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Prism Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 20;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, (Main.DiscoR - Projectile.alpha) * 0.8f / 255f, (Main.DiscoG - Projectile.alpha) * 0.4f / 255f, (Main.DiscoB - Projectile.alpha) * 0f / 255f);
            for (int num339 = 0; num339 < 4; num339++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.noGravity = true;
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f)];
                dust1.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, Main.DiscoColor, true);
            return false;
        }
    }
}