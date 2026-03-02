using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.Toad
{
    public class ShroomGlow : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Mushroom");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 40;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 12;
            NPC.lifeMax = 100;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            if (isDead) 
            {

            }
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, ModContent.DustType<Dusts.ShroomDust>(), default, isDead ? 2f : 1.5f);
            }
        }

        public int body = -1;

        public override void AI()
        {
            NPC.TargetClosest(false);
            if (NPC.alpha > 0)
            {
                NPC.alpha -= 4;
            }
            else
            {
                NPC.alpha = 0;
            }
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, Mod.Find<ModNPC>("TruffleToad").Type, 1000, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC toad = Main.npc[body];
            if (toad == null || toad.life <= 0 || !toad.active || toad.type != Mod.Find<ModNPC>("TruffleToad").Type) { BaseAI.KillNPCWithLoot(NPC); return; }

        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 5)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y > frameHeight * 6)
            {
                NPC.frame.Y = frameHeight * 6;
            }
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void PostAI()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<TruffleToad>()))
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