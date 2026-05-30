using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad
{
    public class LuminousAccordyceps : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Luminous Accordyceps");
            // accord - "to grant or give especially as appropriate, due, or earned" basically giving someone something
            // cordyceps - parasitic mushrooms (though in this case theyre giving someone something)
            // and yes, the singular for cordyceps is cordyceps
            // i used luminous so glowing wasnt spammed too much and to draw attention to this thing since its important in the fight
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
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
            ]);
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
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<TruffleToad>(), 1000, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC toad = Main.npc[body];
            if (toad == null || toad.life <= 0 || !toad.active || toad.type != ModContent.NPCType<TruffleToad>()) { BaseAI.KillNPCWithLoot(NPC); return; }

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