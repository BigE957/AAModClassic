using AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
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

namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs
{
    public class DragonClaw_NPC : ModNPC, IBannerNPC
    {
        public bool WasSpawnedByGripOfChaos = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Claw");
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
            NPC.lavaImmune = true;

            if (WasSpawnedByGripOfChaos)
            {
                NPC.damage = 14;
                NPC.defense = 6;
                NPC.lifeMax = 45;
                NPC.value = 0f;
                NPC.knockBackResist = 0.6f;
                NPC.noTileCollide = true;
            }
            else
            {
                NPC.damage = 2;
                NPC.defense = 8;
                NPC.lifeMax = 25;
                NPC.value = 100f;
                NPC.knockBackResist = 0.4f;
                //Banner = NPC.type;
                //BannerItem = ModContent.ItemType<DragonClawBanner>();
            }

            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.DragonClaw")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ContentReplacementSystem.NeedToReplaceContent || (!Main.dayTime && !AAWorld.downedAkuma))
                return 0f;

            if (spawnInfo.Player.ZoneAnyInferno() && !NPCUtils.AnyEvents(spawnInfo.Player))
                return 0.05f;

            return 0f;
        }

        public override void AI()
        {
            if (WasSpawnedByGripOfChaos)
                AAAI.AIClaw(NPC, ref NPC.ai, true, false, 0.1f, 0.04f, 4, 1.5f, 1f, 1f);
            else
                AAAI.AIClaw(NPC, ref NPC.ai, true, false, 0.1f, 0.04f, 3, 1.5f, 1f, 1f);

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
            return ContentReplacementSystem.NeedToReplaceContent ? 0 : SpawnCondition.OverworldNightMonster.Chance * 0.05f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && !Main.dedServ)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonClawGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonClawGore2").Type, 1f);
                if (!WasSpawnedByGripOfChaos)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonClawGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonClawGore2").Type, 1f);
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }

        public override bool PreKill() => !WasSpawnedByGripOfChaos;

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonClaw_Item>(), 2));
        }
    }
}