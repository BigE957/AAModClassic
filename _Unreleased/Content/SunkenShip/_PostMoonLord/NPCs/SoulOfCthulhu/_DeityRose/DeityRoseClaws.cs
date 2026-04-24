using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose
{
    public class DeityRoseClaws: ModNPC
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ei'Lor's Tentacle");
            Main.npcFrameCount[NPC.type] = 4;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 24;
            NPC.aiStyle = NPCAIStyleID.PlanteraTentacle;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            if (AAModGlobalNPC.Rose < 0)
            {
                //TODOSOC
                //NPC.StrikeNPCNoInteraction(9999, 0f, 0, false, false, false);
                NPC.netUpdate = true;
                return;
            }
            int num750 = AAModGlobalNPC.Rose;
            if (NPC.ai[3] > 0f)
            {
                num750 = (int)NPC.ai[3] - 1;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.localAI[0] -= 1f;
                if (NPC.localAI[0] <= 0f)
                {
                    NPC.localAI[0] = Main.rand.Next(120, 480);
                    NPC.ai[0] = Main.rand.Next(-100, 101);
                    NPC.ai[1] = Main.rand.Next(-100, 101);
                    NPC.netUpdate = true;
                }
            }
            NPC.TargetClosest(true);
            float num751 = 0.2f;
            float num752 = 200f;
            if (Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax * 0.25)
            {
                num752 += 100f;
            }
            if (Main.npc[AAModGlobalNPC.Rose].life < Main.npc[AAModGlobalNPC.Rose].lifeMax * 0.1)
            {
                num752 += 100f;
            }
            if (Main.expertMode)
            {
                float num753 = 1f - NPC.life / (float)NPC.lifeMax;
                num752 += num753 * 300f;
                num751 += 0.3f;
            }
            if (!Main.npc[num750].active || AAModGlobalNPC.Rose < 0)
            {
                NPC.active = false;
                return;
            }
            float num754 = Main.npc[num750].position.X + Main.npc[num750].width / 2;
            float num755 = Main.npc[num750].position.Y + Main.npc[num750].height / 2;
            Vector2 vector93 = new Vector2(num754, num755);
            float num756 = num754 + NPC.ai[0];
            float num757 = num755 + NPC.ai[1];
            float num758 = num756 - vector93.X;
            float num759 = num757 - vector93.Y;
            float num760 = (float)Math.Sqrt((double)(num758 * num758 + num759 * num759));
            num760 = num752 / num760;
            num758 *= num760;
            num759 *= num760;
            if (NPC.position.X < num754 + num758)
            {
                NPC.velocity.X = NPC.velocity.X + num751;
                if (NPC.velocity.X < 0f && num758 > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                }
            }
            else if (NPC.position.X > num754 + num758)
            {
                NPC.velocity.X = NPC.velocity.X - num751;
                if (NPC.velocity.X > 0f && num758 < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                }
            }
            if (NPC.position.Y < num755 + num759)
            {
                NPC.velocity.Y = NPC.velocity.Y + num751;
                if (NPC.velocity.Y < 0f && num759 > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
            }
            else if (NPC.position.Y > num755 + num759)
            {
                NPC.velocity.Y = NPC.velocity.Y - num751;
                if (NPC.velocity.Y > 0f && num759 < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
            }
            if (NPC.velocity.X > 8f)
            {
                NPC.velocity.X = 8f;
            }
            if (NPC.velocity.X < -8f)
            {
                NPC.velocity.X = -8f;
            }
            if (NPC.velocity.Y > 8f)
            {
                NPC.velocity.Y = 8f;
            }
            if (NPC.velocity.Y < -8f)
            {
                NPC.velocity.Y = -8f;
            }
            if (num758 > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2((double)num759, (double)num758);
            }
            if (num758 < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = (float)Math.Atan2((double)num759, (double)num758) + 3.14f;
                return;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                int num440 = 0;
                while (num440 < hit.Damage / (double)NPC.lifeMax * 100.0)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), hit.HitDirection, -1f, 0, default, 1f);
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), 2 * hit.HitDirection, -2f, 0, default, 1f);
                
            }
        }
    }
}