using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Athena
{
    public class SeraphHerald : ModNPC
	{
        public override string Texture => "AAModClassic/NPCs/Bosses/Athena/SeraphA";

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPC.width = 60;
            NPC.height = 40;
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
        }

        int pos;
        public override bool PreAI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (player.Center.X > NPC.Center.X)
            {
                pos = 250;

                NPC.direction = 1;
            }
            else
            {
                pos = -250;

                NPC.direction = -1;
            }

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 200);
            MoveToPoint(wantedVelocity);

            if (Main.netMode != NetmodeID.Server)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += TextureAssets.Npc[NPC.type].Value.Height / 4;
                }
                if (NPC.frame.Y > TextureAssets.Npc[NPC.type].Value.Height / 4 * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Vector2.Distance(player.Center, NPC.Center) > 2200)
                {
                    NPC.position = new Vector2(pos, 200);
                    for (int i = 0; i < 5; i++)
                    {
                        Dust d = Main.dust[Dust.NewDust(NPC.position, NPC.height, NPC.width, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0)];
                        d.position = NPC.Center;
                    }
                }
                NPC.ai[0]++;
                NPC.alpha -= 15;

                if (NPC.ai[0] == 1)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SeraphHerald1"), Color.CadetBlue);
                    NPC.netUpdate = true;
                }
                else
                if (NPC.ai[0] == 120)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SeraphHerald2"), Color.CadetBlue);
                }
                else
                if (NPC.ai[0] == 240)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SeraphHerald3"), Color.CadetBlue);
                }
                else
                if (NPC.ai[0] == 360)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SeraphHerald4"), Color.CadetBlue);
                }
                if (!NPCExtensions.BeenKilled<Greed.Greed>())
                {
                    if (NPC.ai[0] >= 480)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SeraphHerald5"), Color.CadetBlue);

                        for (int i = 0; i < 5; i++)
                        {
                            Dust.NewDust(NPC.position, NPC.height, NPC.width, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0);
                        }

                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    if (NPC.ai[0] == 480)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SeraphHerald6"), Color.CadetBlue);
                    }

                    if (NPC.ai[0] >= 600)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SeraphHerald5"), Color.CadetBlue);

                        for (int i = 0; i < 5; i++)
                        {
                            Dust.NewDust(NPC.position, NPC.height, NPC.width, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0);
                        }

                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = 1;
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
            }
            NPC.rotation = NPC.velocity.X * 0.1f;
            if (NPC.type == NPCID.Bee || NPC.type == NPCID.BeeSmall)
            {
                NPC.frameCounter += 1.0;
                NPC.rotation = NPC.velocity.X * 0.2f;
            }
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter >= 6.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            {
                NPC.frame.Y = 0;
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
    }
}