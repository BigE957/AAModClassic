
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedTransition : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Greed/GreedSpawn";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spark of Greed");
            Main.npcFrameCount[NPC.type] = 4;
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
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        public override void AI()
        {
            NPC.TargetClosest();

            Player player = Main.player[NPC.target];
            MoveToPoint(player.Center - new Vector2(0, 300f));

            if (Main.netMode != 2) //clientside stuff
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

            if (Main.netMode != 1)
            {
                NPC.ai[0]++;

                if (NPC.ai[0] == 175)
                {
                    if (Main.netMode != 1)
                    {
                        BaseUtility.Chat(Lang.BossChat("GreedTransition1"), Color.Goldenrod);
                    }
                    Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GreedA");

                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] == 350)
                {
                    if (Main.netMode != 1)
                    {
                        BaseUtility.Chat(Lang.BossChat("GreedTransition2"), Color.Goldenrod);
                    }
                }
                else if (NPC.ai[0] == 500)
                {
                    if (Main.netMode != 1)
                    {
                        BaseUtility.Chat(Lang.BossChat("GreedTransition3"), Color.Goldenrod);
                    }

                    NPC.netUpdate = true;
                }
                else if (NPC.ai[0] >= 610)
                {
                    AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("GreedA").Type, true, NPC.Center, Lang.BossChat("GreedAName"), false);

                    if (Main.netMode != 1)
                    {
                        BaseUtility.Chat(Lang.BossChat("GreedTransition4"), Color.Goldenrod);
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
            if (!NPC.AnyNPCs(Mod.Find<ModNPC>("GreedA").Type))
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