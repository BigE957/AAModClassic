using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Ammo;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.BossStandard;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Tools;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
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
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma
{
    [AutoloadBossHead]
    public class AkumaBody : AkumaHead
    {
        private static Asset<Texture2D> ArmlessBody;
        private static Asset<Texture2D> UpperArm;
        private static Asset<Texture2D> LowerArm;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Akuma, Draconian Demon");
            Main.npcFrameCount[NPC.type] = 5;
            this.HideFromBestiary();

            ArmlessBody = ModContent.Request<Texture2D>(Texture + "_Armless");
            UpperArm = ModContent.Request<Texture2D>(Texture + "_Arm_Upper");
            LowerArm = ModContent.Request<Texture2D>(Texture + "_Arm_Lower");
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
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 2f);
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
                if (!Main.npc[(int)NPC.ai[1]].active || Main.npc[(int)NPC.ai[3]].type != ModContent.NPCType<AkumaHead>())
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

        public override void FindFrame(int frameHeight) =>  NPC.frame.Y = frameHeight * (int)NPC.ai[2];

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) || NPC.ai[2] != 0)
                return true;

            spriteBatch.Draw(ArmlessBody.Value, NPC.Center - Main.screenPosition, null, drawColor, NPC.rotation, (ArmlessBody.Size() * 0.5f) - (Vector2.UnitX * 6 * NPC.spriteDirection), NPC.scale, NPC.SpriteEffectDirection(true), 0);
            return false;
        }

        internal void DrawBackArm(SpriteBatch spriteBatch, Color drawColor)
        {
            Rectangle upperBackArmFrame = UpperArm.Frame(2, frameX: 1);
            Vector2 upperBackArmPos = NPC.Center + (new Vector2(0 * NPC.spriteDirection, -8).RotatedBy(NPC.rotation + MathHelper.PiOver2) * NPC.scale) - Main.screenPosition;
            float bodyFacingAngle = NPC.rotation;
            Vector2 upperBackArmOrigin = new(4, 8);
            if (NPC.spriteDirection == 1)
                upperBackArmOrigin.X = upperBackArmFrame.Width - upperBackArmOrigin.X;

            float upperBackArmRotation = (MathHelper.Pi / 3f + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f + NPC.whoAmI) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
            float upperBackArmRotationOffset = -1.75f * -NPC.spriteDirection;

            float upperWorldRot = bodyFacingAngle + upperBackArmRotation + upperBackArmRotationOffset;

            spriteBatch.Draw(UpperArm.Value, upperBackArmPos, upperBackArmFrame, NPC.GetAlpha(drawColor), upperWorldRot, upperBackArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);

            // Lower back arm
            Rectangle lowerBackArmFrame = LowerArm.Frame(2, frameX: 1);
            Vector2 lowerBackArmOrigin = new(0, 44);
            if (NPC.spriteDirection == 1)
                lowerBackArmOrigin.X = lowerBackArmFrame.Width - lowerBackArmOrigin.X;

            Vector2 elbowLocalOffset = new Vector2(-8 * -NPC.spriteDirection, -10).RotatedBy(upperBackArmRotation);
            Vector2 elbowLocal = (upperBackArmRotation.ToRotationVector2() * -28f * NPC.spriteDirection + elbowLocalOffset) * NPC.scale;
            Vector2 elbowOffset = elbowLocal.RotatedBy(bodyFacingAngle);
            Vector2 lowerBackArmPos = upperBackArmPos + elbowOffset;

            float lowerBackArmRotation = (MathHelper.PiOver2 + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f + MathHelper.PiOver2 + NPC.whoAmI) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
            float lowerBackArmRotationOffset = (-MathHelper.PiOver2 - MathHelper.Pi / 3f) * -NPC.spriteDirection;

            float lowerWorldRot = bodyFacingAngle + upperBackArmRotation + lowerBackArmRotation + lowerBackArmRotationOffset;

            spriteBatch.Draw(LowerArm.Value, lowerBackArmPos, lowerBackArmFrame, NPC.GetAlpha(drawColor), lowerWorldRot, lowerBackArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);
        }

        internal void DrawFrontArm(SpriteBatch spriteBatch, Color drawColor)
        {
            Rectangle upperFrontArmFrame = UpperArm.Frame(2);

            Vector2 upperFrontArmPos = NPC.Center - Main.screenPosition;
            float frontBodyFacingAngle = NPC.rotation;

            Vector2 upperFrontArmOrigin = new(4, 8);
            if (NPC.spriteDirection == 1)
                upperFrontArmOrigin.X = upperFrontArmFrame.Width - upperFrontArmOrigin.X;

            float upperFrontArmRotation = (MathHelper.Pi / 3f + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f + MathHelper.Pi + MathHelper.PiOver2 + NPC.whoAmI) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
            float upperFrontArmRotationOffset = -1.75f * -NPC.spriteDirection;

            float upperFrontWorldRot = frontBodyFacingAngle + upperFrontArmRotation + upperFrontArmRotationOffset;

            spriteBatch.Draw(UpperArm.Value, upperFrontArmPos, upperFrontArmFrame, NPC.GetAlpha(drawColor), upperFrontWorldRot, upperFrontArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);

            // Lower front arm
            Rectangle lowerFrontArmFrame = LowerArm.Frame(2);
            Vector2 lowerFrontArmOrigin = new(0, 44);
            if (NPC.spriteDirection == 1)
                lowerFrontArmOrigin.X = lowerFrontArmFrame.Width - lowerFrontArmOrigin.X;

            Vector2 lowerFrontArmLocalOffset = new Vector2(-8 * -NPC.spriteDirection, -10).RotatedBy(upperFrontArmRotation);
            Vector2 lowerFrontArmLocal = (upperFrontArmRotation.ToRotationVector2() * -28f * NPC.spriteDirection + lowerFrontArmLocalOffset) * NPC.scale;
            Vector2 lowerFrontArmWorldOffset = lowerFrontArmLocal.RotatedBy(frontBodyFacingAngle);
            Vector2 lowerFrontArmPos = upperFrontArmPos + lowerFrontArmWorldOffset;

            float lowerFrontArmRotation = (MathHelper.PiOver2 + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f + NPC.whoAmI) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
            float lowerFrontArmRotationOffset = (-MathHelper.PiOver2 - MathHelper.Pi / 3f) * -NPC.spriteDirection;

            float lowerFrontWorldRot = frontBodyFacingAngle + upperFrontArmRotation + lowerFrontArmRotation + lowerFrontArmRotationOffset;

            spriteBatch.Draw(LowerArm.Value, lowerFrontArmPos, lowerFrontArmFrame, NPC.GetAlpha(drawColor), lowerFrontWorldRot, lowerFrontArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaHead>()))
                return false;

            NPC.active = false;
            return true;
        }
    }

    public class AkumaArmDrawSystem : ModSystem
    {
        public override void Load()
        {
            On_Main.DoDraw_DrawNPCsBehindTiles += DrawAkumaArms;
        }

        private void DrawAkumaArms(On_Main.orig_DoDraw_DrawNPCsBehindTiles orig, Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if ((npc.type != ModContent.NPCType<AkumaBody>() && npc.type != ModContent.NPCType<AkumaABody>()) || npc.ai[2] != 0)
                    continue;

                if (npc.ModNPC is AkumaBody akumaBody)
                    akumaBody.DrawBackArm(Main.spriteBatch, Lighting.GetColor(npc.Center.ToTileCoordinates()));
                else if (npc.ModNPC is AkumaABody akumaABody)
                    akumaABody.DrawBackArm(Main.spriteBatch, Lighting.GetColor(npc.Center.ToTileCoordinates()));
            }

            Main.spriteBatch.End();

            orig(self);

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if ((npc.type != ModContent.NPCType<AkumaBody>() && npc.type != ModContent.NPCType<AkumaABody>()) || npc.ai[2] != 0)
                    continue;

                if (npc.ModNPC is AkumaBody akumaBody)
                    akumaBody.DrawFrontArm(Main.spriteBatch, Lighting.GetColor(npc.Center.ToTileCoordinates()));
                else if (npc.ModNPC is AkumaABody akumaABody)
                    akumaABody.DrawFrontArm(Main.spriteBatch, Lighting.GetColor(npc.Center.ToTileCoordinates()));
            }

            Main.spriteBatch.End();
        }
    }
}

