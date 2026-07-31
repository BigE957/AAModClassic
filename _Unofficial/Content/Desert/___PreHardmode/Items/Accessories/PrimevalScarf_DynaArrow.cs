using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Assets;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Assets.AssetDirectory;

namespace AAModClassic._Unofficial.Content.Desert.___PreHardmode.Items.Accessories
{
    public class PrimevalScarf_DynaArrow : ModProjectile
	{
        public ref float MyMortalEnemy => ref Projectile.ai[0];
        public ref float BeenAliveTimer => ref Projectile.ai[1];
        public ref float CurrentMode => ref Projectile.ai[2];

        public Vector2 StretchAmount = new Vector2(0.9f, 3.5f);
        public Vector2 OldArrowDirectionNormalized = new Vector2(0, 0);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Djinnado");
		}

		public override void SetDefaults()
		{
            Projectile.width = 10;
            Projectile.height = 10; 

            Projectile.arrow = true;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Default;
            Projectile.timeLeft = 1200;
            Projectile.tileCollide = false;

            Projectile.damage = 15;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void AI()
        {
            Vector2 targetPos = Main.npc[(int)MyMortalEnemy].Center;

            if (CurrentMode == 0)
            {
                Projectile.velocity *= 0.94f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            else if (CurrentMode == 1)
            {
                Projectile.velocity.Y += 0.6f;

                Projectile.rotation += (0.5f * Math.Clamp(BeenAliveTimer / 25, 0, 1)) * Projectile.direction;

                if (Projectile.velocity.Y > 16f)
                {
                    Projectile.velocity.Y = 16f;
                }
            }
            else if (CurrentMode == 2)
            {
                Projectile.extraUpdates = 1;
                if (BeenAliveTimer == 0)
                    Projectile.velocity = Projectile.Center.DirectionTo(targetPos) * 11;

                if (Projectile.Center.Distance(Main.player[Projectile.owner].Center) > 1000 && BeenAliveTimer > 10)
                    Projectile.Kill();

                Projectile.velocity *= 1.04f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }

            if (BeenAliveTimer > 5)
            {
                StretchAmount *= 0.9f;
                Projectile.friendly = true;
            }

            BeenAliveTimer++;

            if (CurrentMode == 0 && BeenAliveTimer > 15)
            {
                CurrentMode = 1;
                BeenAliveTimer = 0;
            }
            else if (CurrentMode == 1 && BeenAliveTimer > 35)
            {
                CurrentMode = 2;
                BeenAliveTimer = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D bloom = ModContent.Request<Texture2D>(AssetDirectory.General.Bloom_Medium).Value;

            Rectangle arrowRect = new Rectangle(0, 0, 10, 28);
            Rectangle sphereRect = new Rectangle(arrowRect.Width, 0, 16, 28);
            Color dynaGlow = new Color(252, 139, 7, 255);

            // bloom
            float bloomScaleX = Math.Max(Projectile.scale * (StretchAmount.X * 0.5f) * 0.8f, 0.2f);
            float bloomScaleY = Math.Max(Projectile.scale * (StretchAmount.Y * 0.5f) * 0.8f, 0.5f);
            Vector2 bloomScale = new Vector2(bloomScaleX, bloomScaleY);
            Main.EntitySpriteDraw(bloom, Projectile.Center - Main.screenPosition, null, new Color(255, 84, 0) * 0.75f, Projectile.rotation, bloom.Size() * 0.5f, bloomScale, SpriteEffects.None);

            // outline
            /*
            for (int i = 0; i < 4; i++)
            {
                Vector2 offset = new Vector2(2, 0).RotatedBy(MathHelper.TwoPi / 4f * i + Projectile.rotation);
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center + offset - Main.screenPosition, sphereRect, Color.Orange, Projectile.rotation, sphereRect.Size() * 0.5f, Projectile.scale * StretchAmount, SpriteEffects.None);
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center + offset - Main.screenPosition, arrowRect, Color.Orange, Projectile.rotation, arrowRect.Size() * 0.5f, Projectile.scale, SpriteEffects.None);
            }
            */

            // arrow
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, arrowRect, Color.White, Projectile.rotation, arrowRect.Size() * 0.5f, Projectile.scale, SpriteEffects.None);

            // ball
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, sphereRect, Color.White, Projectile.rotation, sphereRect.Size() * 0.5f, Projectile.scale * StretchAmount, SpriteEffects.None);

            return false;
        }
    }
}
