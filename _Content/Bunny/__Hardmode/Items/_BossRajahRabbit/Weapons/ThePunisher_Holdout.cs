using System;
using AAModClassic._Content.Bunny.Projectiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons
{
    public class ThePunisher_Holdout : FlailHoldout
    {
        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => MathHelper.PiOver2;

        public override float LaunchSpeed => 16;

        public override int LaunchTimeLimit => 21;

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("The Punisher");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            //Projectile.alpha = 255;
            base.SetDefaults();
        }

        public override void AI()
        {
            /*
            Vector2 vector54 = Main.player[Projectile.owner].Center - Projectile.Center;
            Projectile.rotation = vector54.ToRotation() - 1.57f;
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }
            Main.player[Projectile.owner].itemAnimation = 10;
            Main.player[Projectile.owner].itemTime = 10;
            float arg_1C53D_0 = vector54.X;
            if (vector54.X < 0f)
            {
                Main.player[Projectile.owner].ChangeDir(1);
                Projectile.direction = 1;
            }
            else
            {
                Main.player[Projectile.owner].ChangeDir(-1);
                Projectile.direction = -1;
            }
            Main.player[Projectile.owner].itemRotation = (vector54 * -1f * Projectile.direction).ToRotation();
            Projectile.spriteDirection = vector54.X > 0f ? -1 : 1;
            if (Projectile.ai[0] == 0f && vector54.Length() > 400f)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 1f || Projectile.ai[0] == 2f)
            {
                float num687 = vector54.Length();
                if (num687 > 1500f)
                {
                    Projectile.Kill();
                    return;
                }
                if (num687 > 600f)
                {
                    Projectile.ai[0] = 2f;
                }
                Projectile.tileCollide = false;
                float num688 = 20f;
                if (Projectile.ai[0] == 2f)
                {
                    num688 = 40f;
                }
                Projectile.velocity = Vector2.Normalize(vector54) * num688;
                if (vector54.Length() < num688)
                {
                    Projectile.Kill();
                    return;
                }
            }
            */

            base.AI();

            /*
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] > 5f)
            {
                Projectile.alpha = 0;
            }
            */

            if (CurrentAIState != AIState.Ricochet && CurrentAIState != AIState.Dropping)
            {
                if ((CurrentAIState == AIState.Spinning ? SpinningStateTimer : StateTimer) % 4 == 0 && Projectile.owner == Main.myPlayer)
                {
                    Vector2 vector55 = Projectile.DirectionFrom(Main.player[Projectile.owner].Center) * Main.rand.Next(45, 65) * 0.1f;
                    vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector55 * (CurrentAIState == AIState.Spinning ? 1 : 2), ModContent.ProjectileType<RajahCarrot>(), Projectile.damage, Projectile.knockBack, Projectile.owner, -10f, 0f);
                }
            }
        }

        public override void OnHitNPC (NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (CurrentAIState == AIState.LaunchingForward)
            {
                CurrentAIState = AIState.Retracting;
                StateTimer = 0f;
                Projectile.netUpdate = true;
                Projectile.velocity *= 0.3f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
            /*
            Texture2D texture = Chain.Value;
            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            
            return true;
            */
        }
    }
}