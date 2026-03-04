using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ChaosYariEXShot : ModProjectile
    {
        public bool spineEnd = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perfect Chaos Yari");
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 320;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
        }

        public override void AI()
        {

            BaseAI.AIVilethorn(Projectile, 70, 4, 30);
            spineEnd = Projectile.ai[1] == 30;
            if (spineEnd)
            {
                Projectile.frame = 0;
            }
            else
            {
                Projectile.frame = 1;
            }
        }

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.Server && Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
            {
                for (int j = 0; j < 4; j++)
                {
                    int DustType = ModContent.DustType<Dusts.Discord>();
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType, Projectile.velocity.X * 0.025f, Projectile.velocity.Y * 0.025f, 40, Color.White, j == 0 ? 1.1f : 1.2f);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color newLightColor = new Color(Math.Max(0, Color.Purple.R + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, Color.Purple.G + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, Color.Purple.B + Math.Min(0, -Projectile.alpha + 20)));
            BaseDrawing.AddLight(Projectile.Center, newLightColor);
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 3, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}