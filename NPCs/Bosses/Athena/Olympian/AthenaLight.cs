using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Athena.Olympian
{
    public class AthenaLight : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Angel Clone");
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
			NPC.alpha = 255;
			NPC.dontTakeDamage = true;
            NPC.lifeMax = 2000;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.damage = 70;
            NPC.defense = 60;
            NPC.knockBackResist = 0.2f;
            NPC.width = 152;
            NPC.height = 84;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
        }
        public override void AI()
        {
            bool Athena = NPC.AnyNPCs(ModContent.NPCType<AthenaA>());
            if (!Athena)
            {
                NPC.life = 0;
                NPC.checkDead();
            }
            else
            {
                NPC.alpha = 100;
            }
            Player player = Main.player[NPC.target];
            if (!Main.player[NPC.target].dead)
            {
                Vector2 tPos;
                NPC.ai[1] = 0;
                tPos.X = player.Center.X;
                tPos.Y = player.Center.Y - 70;
                NPC.velocity.X += NPC.DirectionTo(tPos).X * Vector2.Distance(NPC.Center, tPos) / 600 / 2;
                NPC.velocity.Y += NPC.DirectionTo(tPos).Y * Vector2.Distance(NPC.Center, tPos) / 600 / 2 * 3;
            }
            else
            {
                NPC.velocity.Y -= NPC.ai[1];
                NPC.ai[1]++;
                if (NPC.ai[1] > 40 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 6)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y >= frameHeight * 7)
            {
                NPC.frame.Y = 0;
            }
        }
    }
}