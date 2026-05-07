using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Weapons
{
    public class DiamondGreatsword_DiamondBolt : ModProjectile
    {
        public virtual Color LightColor => new Color((255 - Projectile.alpha) * .8f / 255f, (255 - Projectile.alpha) * .8f / 255f, (255 - Projectile.alpha) * .8f / 255f);

        public virtual Color DustColor => Color.Silver;
        public virtual Color DrawColor => Color.White;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Diamond Bolt");
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 20;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, LightColor.R, LightColor.G, LightColor.B);
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            for (int num339 = 0; num339 < 16; num339++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AbyssDust>(), 0, 0, 0, DustColor, 1f)];
                dust1.noGravity = true;
            }
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                dust1 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AbyssDust>(), 0, 0, 0, DustColor, 1f)];
                dust1.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, DrawColor, true);
            return false;
        }
    }
}