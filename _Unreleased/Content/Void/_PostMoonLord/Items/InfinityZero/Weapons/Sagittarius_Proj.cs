using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Sagittarius_Proj : ModProjectile
    {
        public short customGlowMask = 0;
        //TODOIZ
        /*
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = Mod.GetTexture("_Unreleased/Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            Projectile.glowMask = customGlowMask;
        }
        */
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = ProjAIStyleID.HeldProjectile;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.alpha = 0;
            if (Projectile.localAI[1] > 0f)
            {
                Projectile.localAI[1] -= 1f;
            }
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = Projectile.velocity.ToRotation();
            }
            float num32 = Projectile.localAI[0].ToRotationVector2().X >= 0f ? 1 : -1;
            if (Projectile.ai[1] <= 0f)
            {
                num32 *= -1f;
            }
            Vector2 vector17 = (num32 * (Projectile.ai[0] / 30f * 6.28318548f - 1.57079637f)).ToRotationVector2();
            vector17.Y *= (float)Math.Sin(Projectile.ai[1]);
            if (Projectile.ai[1] <= 0f)
            {
                vector17.Y *= -1f;
            }
            vector17 = vector17.RotatedBy(Projectile.localAI[0], default);
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] < 30f)
            {
                Projectile.velocity += 48f * vector17;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
            Vector2 vector24 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector24.X = player.bodyFrame.Width - vector24.X;
            }
            if (player.gravDir != 1f)
            {
                vector24.Y = player.bodyFrame.Height - vector24.Y;
            }
            vector24 -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            Projectile.Center = player.RotatedRelativePoint(player.position + vector24, true) - Projectile.velocity;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Color color25 = Lighting.GetColor((int)(Projectile.position.X + Projectile.width * 0.5) / 16, (int)((Projectile.position.Y + Projectile.height * 0.5) / 16.0));
            if (Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type])
            {
                color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
            }
            Vector2 projPos = Projectile.position;
            projPos = new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition; //fuck it
            Texture2D texture2D22 = TextureAssets.Projectile[Projectile.type].Value;
            Color alpha3 = Projectile.GetAlpha(color25);
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            float num230 = Projectile.velocity.Length() + 16f;
            bool flag24 = num230 < 100f;
            Vector2 value28 = Vector2.Normalize(Projectile.velocity);
            Rectangle rectangle8 = new Rectangle(0, 0, texture2D22.Width, 36); //2 and 40
            Vector2 value29 = new Vector2(0f, Main.player[Projectile.owner].gfxOffY);
            float rotation24 = Projectile.rotation + 3.14159274f;
            Main.spriteBatch.Draw(texture2D22, Projectile.Center.Floor() - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, rectangle8.Size() / 2f - Vector2.UnitY * 4f, Projectile.scale, SpriteEffects.None, 0f);
            num230 -= 40f * Projectile.scale;
            Vector2 vector31 = Projectile.Center.Floor();
            vector31 += value28 * Projectile.scale * 24f;
            rectangle8 = new Rectangle(0, 62, texture2D22.Width, 18); //68 and 18
            if (num230 > 0f)
            {
                float num231 = 0f;
                while (num231 + 1f < num230)
                {
                    if (num230 - num231 < rectangle8.Height)
                    {
                        rectangle8.Height = (int)(num230 - num231);
                    }
                    Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, new Vector2(rectangle8.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0f);
                    num231 += rectangle8.Height * Projectile.scale;
                    vector31 += value28 * rectangle8.Height * Projectile.scale;
                }
            }
            Vector2 value30 = vector31;
            vector31 = Projectile.Center.Floor();
            vector31 += value28 * Projectile.scale * 24f;
            rectangle8 = new Rectangle(0, 40, texture2D22.Width, 20); //46 and 18
            int num232 = 18;
            if (flag24)
            {
                num232 = 9;
            }
            float num233 = num230;
            if (num230 > 0f)
            {
                float num234 = 0f;
                float num235 = num233 / num232;
                num234 += num235 * 0.25f;
                vector31 += value28 * num235 * 0.25f;
                for (int num236 = 0; num236 < num232; num236++)
                {
                    float num237 = num235;
                    if (num236 == 0)
                    {
                        num237 *= 0.75f;
                    }
                    Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, new Vector2(rectangle8.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0f);
                    num234 += num237;
                    vector31 += value28 * num237;
                }
            }
            rectangle8 = new Rectangle(0, 84, texture2D22.Width, 56); //90 and 48
            Main.spriteBatch.Draw(texture2D22, value30 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, texture2D22.Frame(1, 1, 0, 0).Top(), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = Projectile.velocity;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Projectile.localAI[1], Projectile.width * Projectile.scale, new Utils.TileActionAttempt(DelegateMethods.CutTiles));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float num8 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity, 16f * Projectile.scale, ref num8))
            {
                return true;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 300);
        }
    }
}