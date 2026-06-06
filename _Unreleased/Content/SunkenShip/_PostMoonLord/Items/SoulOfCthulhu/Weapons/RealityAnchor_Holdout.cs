using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
    public class RealityAnchor_Holdout : FlailHoldout
    {
        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => base.DrawRotationOffset;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reality Anchor");
            base.SetStaticDefaults();
		}
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.tileCollide = false;
            base.SetDefaults();
        }

        public override void AI()
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position = Projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<CthulhuAuraDust>())];
                dust2 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<CthulhuAuraDust>())];
                dust1.noGravity = true;
                dust2.noGravity = true;
            }

            base.AI();

            /*
            if (Projectile.timeLeft == 120)
            {
                Projectile.ai[0] = 1f;
            }

            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }

            Main.player[Projectile.owner].itemAnimation = 5;
            Main.player[Projectile.owner].itemTime = 5;

            if (Projectile.alpha == 0)
            {
                if (Projectile.position.X + (Projectile.width / 2) > Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2))
                {
                    Main.player[Projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[Projectile.owner].ChangeDir(-1);
                }
            }
            Vector2 vector14 = new Vector2(Projectile.position.X + (Projectile.width * 0.5f), Projectile.position.Y + (Projectile.height * 0.5f));
            float num166 = Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2) - vector14.X;
            float num167 = Main.player[Projectile.owner].position.Y + (Main.player[Projectile.owner].height / 2) - vector14.Y;
            float num168 = (float)Math.Sqrt((num166 * num166) + (num167 * num167));
            if (Projectile.ai[0] == 0f)
            {
                if (num168 > 700f)
                {
                    Projectile.ai[0] = 1f;
                }
                else if (num168 > 500f)
                {
                    Projectile.ai[0] = 1f;
                }
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] > 5f)
                {
                    Projectile.alpha = 0;
                }
                if (Projectile.ai[1] > 8f)
                {
                    Projectile.ai[1] = 8f;
                }
                if (Projectile.ai[1] >= 10f)
                {
                    Projectile.ai[1] = 15f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
                }
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = -1;
                }
                else
                {
                    Projectile.spriteDirection = 1;
                }
            }
            else if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                Projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 30f;

                if (num168 < 50f)
                {
                    Projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                Projectile.velocity.X = num166;
                Projectile.velocity.Y = num167;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                }
                else
                {
                    Projectile.spriteDirection = -1;
                }
            }
            */
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (CurrentAIState == AIState.LaunchingForward)
            {
                int ProjID = Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<RealityAnchor_RealityBurst>(), (int)(Projectile.damage * 1.5f), 0);
                Main.projectile[ProjID].rotation = Projectile.rotation + MathHelper.PiOver2;

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, ProjID, 1f, 0f, 0f, 0, 0, 0);
                }

                CurrentAIState = AIState.Retracting;
                StateTimer = 0f;
                Projectile.netUpdate = true;
                Projectile.velocity *= 0.3f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {		
            return base.PreDraw(ref lightColor);
        }

        public override bool PreDrawFlail(SpriteBatch spriteBatch, Color lightColor, SpriteEffects spriteEffects) => false;

        public override void PostDraw(Color lightColor)
        {
            Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation + MathHelper.PiOver2, TextureAssets.Projectile[Type].Size() * 0.5f, Projectile.scale, Projectile.direction == -1 ? SpriteEffects.FlipHorizontally : 0, 0);
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Main.spriteBatch.Draw(GlowTex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation + MathHelper.PiOver2, GlowTex.Size() * 0.5f, Projectile.scale, Projectile.direction == -1 ? SpriteEffects.FlipHorizontally : 0, 0);
            DrawingUtils.DrawAfterimageWithVelocity(Main.spriteBatch, GlowTex, Projectile.Center - Main.screenPosition, Projectile.velocity, 6, null, AAColor.Cthulhu2, Projectile.scale, [Projectile.rotation + MathHelper.PiOver2], GlowTex.Size() * 0.5f, Projectile.direction == -1 ? SpriteEffects.FlipHorizontally : 0, 0.8f);
        }
    }
}