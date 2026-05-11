using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit
{
    public class RabidRabbit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rabid Rabbit");
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 40;
            NPC.aiStyle = -1;
            NPC.damage = 90;
            NPC.defense = 30;
            NPC.lifeMax = 300;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            for (int m = 0; m < (isDead ? 35 : 6); m++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default, isDead ? 2f : 1.5f);
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            NPC.LookAtTargetWhileNotMovingLookTowardsDirectionWhileMoving();

            NPC.AISlime(ref NPC.ai, false, 25, 6f, -8f, 6f, -10f);
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y < 0)
            {
                NPC.frame.Y = frameHeight * 4;
            }
            else if (NPC.velocity.Y > 0)
            {
                NPC.frame.Y = frameHeight * 5;
            }
            else if (NPC.ai[0] < -15f)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.ai[0] > -15f)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.ai[0] > -10f)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (NPC.ai[0] > -5f)
            {
                NPC.frame.Y = frameHeight * 3;
            }
        }

        public override bool PreKill() => false;

        public override void PostAI()
        {
            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            NPC.oldPos[0] = NPC.position;

            NPC.FadeInOutBasedOnAliveEntities(true, 0, 0, ModContent.NPCType<RajahRabbit>(), ModContent.NPCType<RajahRabbitA>());
        }
    }
}