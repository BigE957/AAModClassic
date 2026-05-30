using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit
{
    public class BunnyBrawler : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bunny Brawler");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.width = 76;
            NPC.height = 76;
            NPC.aiStyle = -1;
            NPC.damage = 120;
            NPC.defense = 60;
            NPC.lifeMax = 400;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit14;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = NPCAIStyleID.Herpling;
            AIType = NPCID.Derpling;
            AnimationType = NPCID.Derpling;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            if (isDead)          //this make so when the npc has 0 life(dead) he will spawn this
            {

            }
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default, isDead ? 2f : 1.5f);
            }
        }
        public bool SetLife = false;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(SetLife);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                SetLife = reader.ReadBoolean(); //Set Lifex
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y == 0)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.velocity.Y < 0)
            {
                NPC.frame.Y = frameHeight;
            }
            else if(NPC.velocity.Y > 0)
            {
                NPC.frame.Y = frameHeight * 2;
            }
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void PostAI()
        {
            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            if (NPC.AnyNPCs(ModContent.NPCType<RajahRabbit>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<RajahRabbitA>()))
            {
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 5;
                }
                else
                {
                    NPC.alpha = 0;
                }
            }
            else
            {
                NPC.dontTakeDamage = true;
                if (NPC.alpha < 255)
                {
                    NPC.alpha += 5;
                }
                else
                {
                    NPC.active = false;
                }
            }
        }
    }
}