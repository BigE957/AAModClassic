using AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs;
using AAModClassic._Content.Madness.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._CrossMod;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using AAModClassic.Utilities.Interfaces;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static AAModClassic._Content.Inferno.___PreHardmode.NPCs.DragonClaw_NPC;

namespace AAModClassic._Content.Mire.___PreHardmode.NPCs
{
    public class HydraClaw_NPC : ModNPC, IBannerNPC
    {
        public bool WasSpawnedByGripOfChaos = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Claw");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            AIType = NPCID.DemonEye;
            AnimationType = NPCID.DemonEye;
            NPC.width = 28;
            NPC.height = 24;
            NPC.friendly = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = -1;
            NPC.noGravity = true;

            if (WasSpawnedByGripOfChaos)
            {
                NPC.damage = 16;
                NPC.defense = 3;
                NPC.lifeMax = 45;
                NPC.value = 0f;
                NPC.knockBackResist = 0.5f;
                NPC.noTileCollide = true;
            }
            else
            {
                NPC.damage = 13;
                NPC.defense = 2;
                NPC.lifeMax = 20;
                NPC.value = 100f;
                NPC.knockBackResist = 0.6f;
                //Banner = NPC.type;
                //BannerItem = ModContent.ItemType<HydraClawBanner>();
            }
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.HydraClaw")
            ]);
        }

        public override void AI()
        {
            if (WasSpawnedByGripOfChaos)
                AAAI.AIClaw(NPC, ref NPC.ai, false, true, 0.1f, 0.04f, 5.5f, 2.5f, 1f, 1f);
            else
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
                if (NPC.frame.Y > 26 * 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void PostAI()
        {
            if (WasSpawnedByGripOfChaos)
                NPC.FadeInOutBasedOnAliveEntities(false, 0, 5, ModContent.NPCType<GripOfChaosInferno>(), ModContent.NPCType<GripOfChaosMire>());
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return ContentReplacementSystem.NeedToReplaceContent ? 0 : SpawnCondition.OverworldNightMonster.Chance * 0.04f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && !Main.dedServ)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HydraClawGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HydraClawGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HydraClawGore3").Type, 1f);
                if (!WasSpawnedByGripOfChaos)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HydraClawGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HydraClawGore3").Type, 1f);
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule spawnedByGrips = new(new SpawnedByGripsMire());

            spawnedByGrips.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraClaw_Item>(), 2));

            npcLoot.Add(spawnedByGrips);
        }

        public class SpawnedByGripsMire : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                HydraClaw_NPC claw = info.npc.ModNPC as HydraClaw_NPC;
                return !claw.WasSpawnedByGripOfChaos;
            }

            public bool CanShowItemDropInUI() => true;

            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.SpawnedByGrips");
        }
    }
}