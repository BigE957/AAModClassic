using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Greed
{
    public class GreedTransition : ModNPC
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Greed/GreedSpawn";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spark of Greed");
            Main.npcFrameCount[NPC.type] = 4;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            Music = MusicManagementSystem.MusicSlots["Silence"];
            NPC.boss = true;
        }

        public override void AI()
        {
            NPC.TargetClosest();

            Player player = Main.player[NPC.target];
            MoveToPoint(player.Center - new Vector2(0, 300f));

            if (Main.netMode != NetmodeID.Server) //clientside stuff
            {
                if (NPC.ai[0] > 175)
                {
                    NPC.alpha -= 3;
                    if (NPC.alpha < 0)
                    {
                        NPC.alpha = 0;
                    }
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0]++;

                if (NPC.ai[0] == 175)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Transition.1"), Color.Goldenrod);
                    }
                    Music = MusicManagementSystem.MusicSlots["Greed_Awakened"];

                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] == 350)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Transition.2"), Color.Goldenrod);
                    }
                }
                else if (NPC.ai[0] == 500)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Transition.3"), Color.Goldenrod);
                    }

                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] >= 610)
                {
                    AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GreedA>(), true, NPC.Center, ModContent.GetInstance<GreedA>().DisplayName.Value, false);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.Transition.4"), Color.Goldenrod);
                    }

                    NPC.netUpdate = true;
                    NPC.active = false;
                }
            }
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<GreedA>()))
            {
                return false;
            }

            NPC.active = false;
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter >= 4)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 70, 70);
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn"), 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, 0, NPC.spriteDirection, 4, SunFrame, NPC.GetAlpha(AAColor.COLOR_WHITEFADE1), true);
            return false;
        }
    }
}