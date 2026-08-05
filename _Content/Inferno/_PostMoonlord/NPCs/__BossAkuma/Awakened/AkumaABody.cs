using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.BossStandard;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Achievements;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened
{
    [AutoloadBossHead]
    public class AkumaABody : AkumaAHead
    {
        public static Asset<Texture2D> ArmlessBody;
        public static Asset<Texture2D> UpperArm;
        public static Asset<Texture2D> LowerArm;
        public static Asset<Texture2D> ArmlessBodyGlow;
        public static Asset<Texture2D> UpperArmGlow;
        public static Asset<Texture2D> LowerArmGlow;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oni Akuma");
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();

            ArmlessBody = ModContent.Request<Texture2D>(Texture + "_Armless");
            UpperArm = ModContent.Request<Texture2D>(Texture + "_Arm_Upper");
            LowerArm = ModContent.Request<Texture2D>(Texture + "_Arm_Lower");
            ArmlessBodyGlow = ModContent.Request<Texture2D>(Texture + "_Armless_Glow");
            UpperArmGlow = ModContent.Request<Texture2D>(Texture + "_Arm_Upper_Glow");
            LowerArmGlow = ModContent.Request<Texture2D>(Texture + "_Arm_Lower_Glow");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.boss = false;
            NPC.width = 40;
            NPC.height = 40;
            NPC.dontCountMe = true;
            NPC.chaseable = false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)NPC.ai[1]].Center;
            Vector2 directionVector = chasePosition - NPC.Center;
            NPC.spriteDirection = (directionVector.X > 0f) ? 1 : -1;
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }


            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active || Main.npc[(int)NPC.ai[3]].type != ModContent.NPCType<AkumaAHead>())
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }

                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
            return false;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers) => modifiers.TargetDamageMultiplier *= .1f;

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

        public override bool PreKill() => false;

        public override void FindFrame(int frameHeight) => NPC.frame.Y = frameHeight * (int)NPC.ai[2];

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) || NPC.ai[2] != 0)
                return base.PreDraw(spriteBatch, screenPos, drawColor);

            spriteBatch.Draw(ArmlessBody.Value, NPC.Center - screenPos, null, drawColor * NPC.Opacity, NPC.rotation, (ArmlessBody.Size() * 0.5f), NPC.scale, NPC.SpriteEffectDirection(true), 0);

            int shader;
            if (NPC.ai[1] == 1 || NPC.ai[2] >= 470 || Main.npc[(int)NPC.ai[3]].ai[1] == 1 || Main.npc[(int)NPC.ai[3]].ai[2] >= 500)
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            else
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (spriteBatch) => {
                spriteBatch.Draw(ArmlessBodyGlow.Value, NPC.Center - screenPos, null, Color.White * NPC.Opacity, NPC.rotation, (ArmlessBody.Size() * 0.5f), NPC.scale, NPC.SpriteEffectDirection(true), 0);
            });
            return false;
        }

        public void DrawBackArm(SpriteBatch spriteBatch, Color drawColor) => DrawBackArm(spriteBatch, NPC.Center - Main.screenPosition, NPC.GetAlpha(drawColor), NPC.rotation, NPC.spriteDirection, NPC.scale, Main.GlobalTimeWrappedHourly * 3f + NPC.whoAmI, NPC.ai[1] == 1 || NPC.ai[2] >= 470 || Main.npc[(int)NPC.ai[3]].ai[1] == 1 || Main.npc[(int)NPC.ai[3]].ai[2] >= 500);

        public static void DrawBackArm(SpriteBatch spriteBatch, Vector2 center, Color drawColor, float rotation, int dir, float scale, float time, bool flaming)
        {
            Rectangle upperBackArmFrame = UpperArm.Frame(2, frameX: 1);
            
            Vector2 upperBackArmPos = center + (new Vector2(0 * dir, -8).RotatedBy(rotation + MathHelper.PiOver2) * scale);
            float bodyFacingAngle = rotation;
            
            Vector2 upperBackArmOrigin = new(12, 8);
            if (dir == 1)
                upperBackArmOrigin.X = upperBackArmFrame.Width - upperBackArmOrigin.X;

            float upperBackArmRotation = (MathHelper.Pi / 3f + MathF.Sin(time) * MathHelper.Pi / 8f) * -dir;
            float upperBackArmRotationOffset = -1.75f * -dir;

            float upperWorldRot = bodyFacingAngle + upperBackArmRotation + upperBackArmRotationOffset;

            spriteBatch.Draw(UpperArm.Value, upperBackArmPos, upperBackArmFrame, drawColor, upperWorldRot, upperBackArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            int shader;
            if (flaming)
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            else
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (spriteBatch) => {
                spriteBatch.Draw(UpperArmGlow.Value, upperBackArmPos, upperBackArmFrame, Color.White, upperWorldRot, upperBackArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            });

            // Lower back arm
            Rectangle lowerBackArmFrame = LowerArm.Frame(2, frameX: 1);
            Vector2 lowerBackArmOrigin = new(0, 44);
            if (dir == 1)
                lowerBackArmOrigin.X = lowerBackArmFrame.Width - lowerBackArmOrigin.X;

            Vector2 elbowLocalOffset = new Vector2(-8 * -dir, -10).RotatedBy(upperBackArmRotation);
            Vector2 elbowLocal = (upperBackArmRotation.ToRotationVector2() * -28f * dir + elbowLocalOffset) * scale;
            Vector2 elbowOffset = elbowLocal.RotatedBy(bodyFacingAngle);
            Vector2 lowerBackArmPos = upperBackArmPos + elbowOffset;

            float lowerBackArmRotation = (MathHelper.PiOver2 + MathF.Sin(time + MathHelper.PiOver2) * MathHelper.Pi / 8f) * -dir;
            float lowerBackArmRotationOffset = (-MathHelper.PiOver2 - MathHelper.Pi / 3f) * -dir;

            float lowerWorldRot = bodyFacingAngle + upperBackArmRotation + lowerBackArmRotation + lowerBackArmRotationOffset;

            spriteBatch.Draw(LowerArm.Value, lowerBackArmPos, lowerBackArmFrame, drawColor, lowerWorldRot, lowerBackArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (spriteBatch) => {
                spriteBatch.Draw(LowerArmGlow.Value, lowerBackArmPos, lowerBackArmFrame, Color.White, lowerWorldRot, lowerBackArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            });
        }

        public void DrawFrontArm(SpriteBatch spriteBatch, Color drawColor) => DrawFrontArm(spriteBatch, NPC.Center - Main.screenPosition, NPC.GetAlpha(drawColor), NPC.rotation, NPC.spriteDirection, NPC.scale, Main.GlobalTimeWrappedHourly * 3f + NPC.whoAmI, NPC.ai[1] == 1 || NPC.ai[2] >= 470 || Main.npc[(int)NPC.ai[3]].ai[1] == 1 || Main.npc[(int)NPC.ai[3]].ai[2] >= 500);

        public static void DrawFrontArm(SpriteBatch spriteBatch, Vector2 center, Color drawColor, float rotation, int dir, float scale, float time, bool flaming)
        {
            Rectangle upperFrontArmFrame = UpperArm.Frame(2);

            Vector2 upperFrontArmPos = center;
            float frontBodyFacingAngle = rotation;

            Vector2 upperFrontArmOrigin = new(12, 8);
            if (dir == 1)
                upperFrontArmOrigin.X = upperFrontArmFrame.Width - upperFrontArmOrigin.X;

            float upperFrontArmRotation = (MathHelper.Pi / 3f + MathF.Sin(time + MathHelper.Pi + MathHelper.PiOver2) * MathHelper.Pi / 8f) * -dir;
            float upperFrontArmRotationOffset = -1.75f * -dir;

            float upperFrontWorldRot = frontBodyFacingAngle + upperFrontArmRotation + upperFrontArmRotationOffset;

            spriteBatch.Draw(UpperArm.Value, upperFrontArmPos, upperFrontArmFrame, drawColor, upperFrontWorldRot, upperFrontArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            int shader;
            if (flaming)
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            else
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (spriteBatch) => {
                spriteBatch.Draw(UpperArmGlow.Value, upperFrontArmPos, upperFrontArmFrame, Color.White, upperFrontWorldRot, upperFrontArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            });

            // Lower front arm
            Rectangle lowerFrontArmFrame = LowerArm.Frame(2);
            Vector2 lowerFrontArmOrigin = new(0, 44);
            if (dir == 1)
                lowerFrontArmOrigin.X = lowerFrontArmFrame.Width - lowerFrontArmOrigin.X;

            Vector2 lowerFrontArmLocalOffset = new Vector2(-8 * -dir, -10).RotatedBy(upperFrontArmRotation);
            Vector2 lowerFrontArmLocal = (upperFrontArmRotation.ToRotationVector2() * -28f * dir + lowerFrontArmLocalOffset) * scale;
            Vector2 lowerFrontArmWorldOffset = lowerFrontArmLocal.RotatedBy(frontBodyFacingAngle);
            Vector2 lowerFrontArmPos = upperFrontArmPos + lowerFrontArmWorldOffset;

            float lowerFrontArmRotation = (MathHelper.PiOver2 + MathF.Sin(time) * MathHelper.Pi / 8f) * -dir;
            float lowerFrontArmRotationOffset = (-MathHelper.PiOver2 - MathHelper.Pi / 3f) * -dir;

            float lowerFrontWorldRot = frontBodyFacingAngle + upperFrontArmRotation + lowerFrontArmRotation + lowerFrontArmRotationOffset;

            spriteBatch.Draw(LowerArm.Value, lowerFrontArmPos, lowerFrontArmFrame, drawColor, lowerFrontWorldRot, lowerFrontArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (spriteBatch) => {
                spriteBatch.Draw(LowerArmGlow.Value, lowerFrontArmPos, lowerFrontArmFrame, Color.White, lowerFrontWorldRot, lowerFrontArmOrigin, scale, dir == 1 ? SpriteEffects.FlipHorizontally : 0, 0);
            });
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaAHead>()))
                return false;

            NPC.active = false;
            return true;
        }
    }
}
