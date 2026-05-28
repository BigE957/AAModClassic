using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs
{
    public class TinyToad : ModNPC
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
            NPC.alpha = 255;
            if (!WasSpawnedByTruffleToad)
            {
                Banner = NPC.type;
                BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.TinyToadBanner>();
            }
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

        public override void FindFrame(int frameHeight) => NPC.FrameHandler_HostileFrog(frameHeight);

        public override void PostAI()
        {
            if (WasSpawnedByTruffleToad)
                NPC.FadeInOutBasedOnAliveEntities(true, 4, 0, ModContent.NPCType<TruffleToad>());
            else
                NPC.FadeInOutBasedOnAliveEntities(true, -1, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneGlowshroom && NPCExtensions.BeenKilled<FeudalFungus>() ? .3f : 0f;
        }

        public override void OnKill()
        {
            if (!WasSpawnedByTruffleToad) 
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<GlowingMushium>(), Main.rand.Next(1, 5));
        }
    }
}