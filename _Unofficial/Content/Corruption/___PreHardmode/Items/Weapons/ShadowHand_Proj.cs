using AAModClassic.Assets;
using AAModClassic.Particles;
using AAModClassic.Particles.Types;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Corruption.___PreHardmode.Items.Weapons
{
    public class ShadowHand_Proj : ModProjectile
    {
        private static Asset<Texture2D> Chain;

        public ref float ModeTimer => ref Projectile.ai[0];
        public ref float CurrentMode => ref Projectile.ai[1];
        public ref float AmountOfTimesHit => ref Projectile.ai[2];

        public enum ModeEnum
        {
            Reach = 0,
            PullFail = 1,
            Latch = 2,
            PullKilledEnemy = 3,
            Snatch = 4
        }

        public NPC GuyImHookedOnto;
        public Vector2 StartPos = Vector2.Zero;
        public Vector2 EndPos = Vector2.Zero;
        public Vector2 PositionRelativeToTargetCenter = Vector2.Zero;
        public bool FlipSprite = Main.rand.NextBool();

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Venom");
		}

        public override void Load()
        {
           Chain = ModContent.Request<Texture2D>(Texture + "_Chain");
        }

        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            int REACHLENGTH = 50;
            int PULLLENGTH = 50;

            if (CurrentMode == (int)ModeEnum.Reach)
            {
                if (ModeTimer == 0)
                {
                    StartPos = Projectile.Center;
                    EndPos = Main.MouseWorld + new Vector2(Main.rand.NextFloat(-30, 30), Main.rand.NextFloat(-30, 30));
                }

                float percent = MathHelper.Clamp(ModeTimer / REACHLENGTH, 0, 1);
                percent = MathUtils.SineOutEasing(percent);
                Projectile.Center = Vector2.Lerp(StartPos, EndPos, percent);

                if (percent == 1)
                {
                    CurrentMode = (int)ModeEnum.Snatch;
                    ModeTimer = 0;
                }
            }
            else if (CurrentMode == (int)ModeEnum.PullFail)
            {
                Projectile.friendly = false;

                float percent = MathHelper.Clamp(ModeTimer / PULLLENGTH, 0, 1);
                percent = MathUtils.ExpInEasing(percent);
                Projectile.Center = Vector2.Lerp(EndPos, StartPos, percent);

                if (percent == 1)
                    Projectile.Kill();
            }
            else if (CurrentMode == (int)ModeEnum.PullKilledEnemy)
            {
                Projectile.friendly = false;

                if (ModeTimer == 1)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 speed = Projectile.Center.DirectionTo(StartPos).SafeNormalize(Vector2.Zero) * 2;
                        speed.X *= Main.rand.NextFloat(1f, 2.5f);
                        speed.Y *= Main.rand.NextFloat(1f, 2.5f);
                        speed.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4));
                        ParticleSystem.SpawnParticle(new CircleGlow(Projectile.Center, speed, 1.25f, Color.Purple, 0.94f, 0, 0.98f, false, true, false));
                    }
                }

                float percent = MathHelper.Clamp(ModeTimer / PULLLENGTH, 0, 1);
                percent = MathUtils.SineBumpEasing(percent);
                Projectile.Center = Vector2.Lerp(EndPos, StartPos, percent);

                if (percent == 1)
                    Projectile.Kill();

            }
            else if (CurrentMode == (int)ModeEnum.Snatch)
            {
                Projectile.friendly = true;

                if (ModeTimer >= 15)
                {
                    CurrentMode = (int)ModeEnum.PullFail;
                    ModeTimer = 0;
                }
            }
            else if (CurrentMode == (int)ModeEnum.Latch)
            {
                Projectile.Center = Main.npc[GuyImHookedOnto.whoAmI].Center + PositionRelativeToTargetCenter;

                if (AmountOfTimesHit >= 10 || (Main.npc[GuyImHookedOnto.whoAmI].active == false || Main.npc[GuyImHookedOnto.whoAmI].type != GuyImHookedOnto.type))
                {
                    CurrentMode = (int)ModeEnum.PullKilledEnemy;
                    EndPos = Projectile.Center;
                    ModeTimer = 0;
                }
            }

            Projectile.rotation = StartPos.DirectionTo(EndPos).ToRotation() + MathHelper.PiOver2;
            ModeTimer++;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            CurrentMode = (int)ModeEnum.Latch;
            if (PositionRelativeToTargetCenter == Vector2.Zero)
                PositionRelativeToTargetCenter = Projectile.Center - target.Center;
            AmountOfTimesHit++;
            GuyImHookedOnto = target;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle handOpenRect = new Rectangle(0, 0, 28, 34);
            if (CurrentMode != (int)ModeEnum.Reach)
                handOpenRect.X = handOpenRect.Width;

            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, handOpenRect, Color.White, Projectile.rotation, handOpenRect.Size() * 0.5f, Projectile.scale, FlipSprite ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            return false;
        }
    }
}