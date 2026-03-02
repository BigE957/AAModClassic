using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Grips
{
    public class HydraClawM : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Claw");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            AIType = NPCID.DemonEye;  //npc behavior
            AnimationType = NPCID.DemonEye;
            NPC.width = 28;
            NPC.height = 24;
            NPC.friendly = false;
            NPC.damage = 16;
            NPC.defense = 3;
            NPC.lifeMax = 45;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<GripOfChaosRed>()) && !NPC.AnyNPCs(ModContent.NPCType<GripOfChaosBlue>()))
            {
                NPC.alpha += 10;
                if (NPC.alpha > 255)
                {
                    NPC.active = false;
                }
            }
            AAAI.AIClaw(NPC, ref NPC.ai, false, true, 0.1f, 0.04f, 5.5f, 2.5f, 1f, 1f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore4"), 1f);
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }
    }
}