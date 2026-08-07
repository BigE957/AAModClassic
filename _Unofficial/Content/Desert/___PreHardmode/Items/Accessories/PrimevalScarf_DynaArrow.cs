using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Assets;
using AAModClassic.Particles;
using AAModClassic.Particles.Types;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Assets.AssetDirectory;

namespace AAModClassic._Unofficial.Content.Desert.___PreHardmode.Items.Accessories
{
    public class PrimevalScarf_DynaArrow : ModProjectile
	{
        public ref float MyMortalEnemy => ref Projectile.ai[0];
        public ref float ModeTimer => ref Projectile.ai[1];
        public ref float CurrentMode => ref Projectile.ai[2];

        public Vector2 StretchAmount = new Vector2(0.9f, 3.5f);
        public Vector2 OldArrowDirectionNormalized = new Vector2(0, 0);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Djinnado");

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
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
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void AI()
        {
            Vector2 targetPos = Main.npc[(int)MyMortalEnemy].Center;

            if (CurrentMode == 0 && ModeTimer == 0)
            {
                SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Custom/MagicDing") with { Pitch = 1, PitchVariance = 0.8f, MaxInstances = 0 }, Projectile.Center);
            }

            if (CurrentMode == 0 && ModeTimer > 15)
            {
                CurrentMode = 1;
                ModeTimer = 0;
            }
            else if (CurrentMode == 1 && ModeTimer > 35)
            {
                CurrentMode = 2;
                ModeTimer = 0;

                Projectile.oldPos = new Vector2[Projectile.oldPos.Length];
                Projectile.oldRot = new float[Projectile.oldRot.Length];
            }

            if (CurrentMode == 0)
            {
                Projectile.velocity *= 0.94f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            else if (CurrentMode == 1)
            {
                Projectile.velocity.Y += 0.6f;

                Projectile.rotation += (0.5f * Math.Clamp(ModeTimer / 25, 0, 1)) * Projectile.direction;

                if (Projectile.velocity.Y > 16f)
                {
                    Projectile.velocity.Y = 16f;
                }
            }
            else if (CurrentMode == 2)
            {
                Projectile.extraUpdates = 2;
                if (ModeTimer == 0)
                {
                    Projectile.velocity = Projectile.Center.DirectionTo(targetPos) * 7;
                    Projectile.oldPos = new Vector2[Projectile.oldPos.Length];

                    for (int i = 0; i < 6; i++)
                    {
                        SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Custom/WooshTiny") with { Pitch = 1, PitchVariance = 0.5f, MaxInstances = 0, Volume = 0.33f }, Projectile.Center);

                        Vector2 speed = Projectile.Center.DirectionFrom(targetPos) * Main.rand.NextFloat(2, 3);
                        ParticleSystem.SpawnParticle(new CircleGlow(Projectile.Center, speed, 1, new Color(255, 157, 0), 0.88f, 0, 0.98f, false, true, true));

                        speed = Projectile.Center.DirectionFrom(targetPos) * Main.rand.NextFloat(3, 6);
                        speed.X *= Main.rand.NextFloat(0.2f, 0.4f);
                        speed.Y *= Main.rand.NextFloat(0.2f, 0.4f);
                        speed = speed.RotatedBy(Main.rand.NextFloat(-0.8f, 0.8f));
                        ParticleSystem.SpawnParticle(new CircleGlow(Projectile.Center, speed, 1, new Color(255, 157, 0), 0.86f, 0, 0.98f, false, true, true));
                    }
                }

                if (Projectile.Center.Distance(Main.player[Projectile.owner].Center) > 1000 && ModeTimer > 10)
                    Projectile.Kill();

                Projectile.velocity *= 1.04f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }

            if (ModeTimer > 5)
            {
                StretchAmount *= 0.9f;
                Projectile.friendly = true;
            }

            ModeTimer++;
        }

        public override void OnKill(int timeLeft)
        {
            NPC target = Main.npc[(int)MyMortalEnemy];

            for (int i = 0; i < 8; i++)
            {
                Vector2 speed = -Projectile.velocity.SafeNormalize(Vector2.Zero) * 2;
                speed.X *= Main.rand.NextFloat(1f, 2.5f);
                speed.Y *= Main.rand.NextFloat(1f, 2.5f);
                speed.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4));
                int biggerBox = target.DirectionTo(Projectile.Center - Projectile.velocity).X > target.DirectionTo(Projectile.Center - Projectile.velocity).Y ? target.width / 2 : target.height / 2;
                Vector2 center = target.Center + target.DirectionTo(Projectile.Center - Projectile.velocity) * biggerBox;
                ParticleSystem.SpawnParticle(new CircleGlow(center + (Projectile.velocity.SafeNormalize(Vector2.Zero) * 2), speed, 1.25f, new Color(255, 157, 0), 0.94f, 0, 0.98f, false, true, true));
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D bloom = ModContent.Request<Texture2D>(General.Bloom_Medium).Value;

            Rectangle arrowRect = new Rectangle(0, 0, 10, 28);
            Rectangle sphereRect = new Rectangle(arrowRect.Width, 0, 16, 28);
            Color dynaGlow = new Color(252, 139, 7, 255);
            float FixTrailGoingBehindPlayerOnSpawn = CurrentMode == 0 && ModeTimer <= 2 ? 0.5f : 1f;
            Vector2 arrowScale = new Vector2(Projectile.scale, Projectile.scale);
            if (CurrentMode == 2)
            {
                arrowScale.Y *= MathHelper.Lerp(1, 2f, Math.Clamp(Projectile.velocity.Length() / 25, 0, 25));
                
                // afteriamge 
                DrawingUtils.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, Projectile.oldPos, arrowRect, Color.White, arrowScale, Projectile.oldRot, arrowRect.Size() * 0.5f, SpriteEffects.None, 2f, 0.85f, Projectile.width / (2f * Projectile.extraUpdates), Projectile.height / (2f * Projectile.extraUpdates));
            }

            // bloom
            float bloomScaleX = Math.Max(Projectile.scale * (StretchAmount.X * 0.5f) * 0.8f, 0.2f);
            float bloomScaleY = Math.Max(Projectile.scale * (StretchAmount.Y * 0.5f) * 0.8f, 0.5f);
            Vector2 bloomScale = new Vector2(bloomScaleX, bloomScaleY) * FixTrailGoingBehindPlayerOnSpawn;
            if (CurrentMode == 2)
                bloomScale.Y *= MathHelper.Lerp(1, 2f, Math.Clamp(Projectile.velocity.Length() / 25, 0, 25));
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
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, arrowRect, Color.White, Projectile.rotation, arrowRect.Size() * 0.5f, arrowScale, SpriteEffects.None);

            // ball
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, sphereRect, Color.White, Projectile.rotation, sphereRect.Size() * 0.5f, Projectile.scale * StretchAmount * FixTrailGoingBehindPlayerOnSpawn, SpriteEffects.None);

            return false;
        }
    }
}
