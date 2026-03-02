using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAMod.NPCs.Enemies.Other
{
    public class HydraClaw : ModNPC
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
            NPC.damage = 13;
            NPC.defense = 2;
            NPC.lifeMax = 20;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100f;
            NPC.knockBackResist = 0.6f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("HydraClawBanner").Type;
        }

        public override void AI()
        {
            AAAI.AIClaw(NPC, ref NPC.ai, false, true, 0.1f, 0.04f, 5f, 2f, 1f, 1f);
            if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 3.14f;
            }

            NPC.frameCounter++;
            if (NPC.frameCounter >= 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 26;
                if (NPC.frame.Y > (26 * 4))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.04f;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/HydraClawGore3"), 1f);
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }

        public override void OnKill()
        {
            if(Main.rand.NextBool())
            {
                NPC.DropLoot(Mod.Find<ModItem>("HydraClaw").Type, 1);
            }
            
        }
    }
}