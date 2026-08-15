using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad;
using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using AAModClassic.Utilities.Interfaces;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;


namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs
{
    public class TinyToad : ModNPC, IBannerNPC
    {
        public bool WasSpawnedByTruffleToad = false;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tiny Toad");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 28;
            NPC.aiStyle = -1;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            if (WasSpawnedByTruffleToad)
                Banner = 0;
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
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                int dustType = ModContent.DustType<Dusts.ShroomDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default, isDead ? 2f : 1.5f);
            }
        }
        
        public override void AI()
        {
            NPC.TargetClosest(true);
            NPC.LookAtTargetWhileNotMovingLookTowardsDirectionWhileMoving();

            if (NPC.ai[0] < -10) 
                NPC.ai[0] = -10; //force rapid jumping

            NPC.AISlime(ref NPC.ai, false, 30, 6f, -6f, 6f, -8f);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.FrameHandler_HostileFrog(frameHeight);
            if (NPC.IsABestiaryIconDummy)
                NPC.alpha = 0;
        }

        public override void PostAI()
        {
            if (WasSpawnedByTruffleToad)
                NPC.FadeInOutBasedOnAliveEntities(true, 4, 0, ModContent.NPCType<TruffleToad>());
            else
                NPC.FadeInOutBasedOnAliveEntities(true, -1, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneGlowshroom && AADowned.downedFeudalFungus ? .3f : 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule spawnedByTruffleToad = new(new SpawnedByTruffleToad());

            spawnedByTruffleToad.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GlowingMushium>(), 1, 1, 5));

            npcLoot.Add(spawnedByTruffleToad);
        }

        public class SpawnedByTruffleToad : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                TinyToad toad = info.npc.ModNPC as TinyToad;
                return !toad.WasSpawnedByTruffleToad;
            }

            public bool CanShowItemDropInUI() => true;

            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.SpawnedByTruffleToad");
        }
    }
}