using AAModClassic._Content.Bunny.Projectiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items._BossEmperorFishron.Weapons
{
    public class EmperorFlairon_Holdout : FlailHoldout
    {
        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => MathHelper.PiOver2;

        public override float LaunchSpeed => base.LaunchSpeed;

        public override int LaunchTimeLimit => 25;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Emperor Flairon");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            //Projectile.aiStyle = ProjAIStyleID.Flairon;
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
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] > 5f)
            {
                Projectile.alpha = 0;
            }
            if ((int)Projectile.ai[1] % 3 == 0 && Projectile.owner == Main.myPlayer)
            {
                Vector2 vector55 = vector54 * -1f;
                vector55.Normalize();
                vector55 *= Main.rand.Next(45, 65) * 0.1f;
                vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector55.X, vector55.Y, ProjectileID.FlaironBubble, Projectile.damage, Projectile.knockBack, Projectile.owner, -10f, 0f);
                return;
            }
            */

            base.AI();

            if (CurrentAIState != AIState.Ricochet && CurrentAIState != AIState.Dropping)
            {
                if ((CurrentAIState == AIState.Spinning ? SpinningStateTimer : StateTimer) % 3 == 0 && Projectile.owner == Main.myPlayer)
                {
                    Vector2 vector55 = Projectile.DirectionFrom(Main.player[Projectile.owner].Center) * Main.rand.Next(45, 65) * 0.1f;
                    vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector55, ProjectileID.FlaironBubble, Projectile.damage, Projectile.knockBack, Projectile.owner, -10f, 0f);
                }
            }
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			for (int h = 0; h < 6; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, ProjectileID.FlaironBubble, Projectile.damage, 0, Main.myPlayer);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            /*
			for (int h = 0; h < 6; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(null), Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, ProjectileID.FlaironBubble, Projectile.damage, 0, Main.myPlayer);
			}
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            if (Projectile.type == ProjectileID.ThornChakram || Projectile.type == ProjectileID.LightDisc)
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            else
            {
                Projectile.ai[0] = 1f;
            }
            Projectile.netUpdate = true;
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            */
            return base.OnTileCollide(oldVelocity);
		}
		
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
            /*
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Chains/EFlairon_Chain").Value;

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
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
            */
        }
    }
}