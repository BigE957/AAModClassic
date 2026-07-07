using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.AncientLung
{
    public class AncientLungBody : AncientLungHead, IBannerNPC
    {
        public int OverrideBannerNPCType => ModContent.NPCType<AncientLungHead>();

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Lung");
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
            NPC.alpha = 255;
            Banner = NPC.type;
            //BannerItem = ModContent.ItemType<AncientLungBanner>();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }


        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)NPC.ai[1]].Center;
            Vector2 directionVector = chasePosition - NPC.Center;
            NPC.spriteDirection = directionVector.X > 0f ? 1 : -1;
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            AAAI.DustOnNPCSpawn(NPC, ModContent.DustType<Dusts.AkumaDust>(), 2, 12);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active || Main.npc[(int)NPC.ai[3]].type != ModContent.NPCType<AncientLungHead>())
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

            Player player = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<AncientLungHead>()))
            {
                return false;
            }
            return true;
        }
    }
}
