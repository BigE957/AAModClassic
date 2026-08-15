using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using AAModClassic.Utilities.Interfaces;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.RedMushroom.___PreHardmode.NPCs
{
    public class FungusFrog : ModNPC, IBannerNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fungus Frog");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 28;
            NPC.aiStyle = -1;
            NPC.damage = 8;
            NPC.defense = 6;
            NPC.lifeMax = 50;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.FungusFrogBanner>();
            SpawnModBiomes = [ModContent.GetInstance<RedMushroomBiome>().Type];
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                int dustType = ModContent.DustType<Dusts.MushDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default, isDead ? 2f : 1.5f);
            }
        }
        
        public override void AI()
        {
            NPC.TargetClosest(true);
            NPC.LookAtTargetWhileNotMovingLookTowardsDirectionWhileMoving();

            if (NPC.ai[0] < -10) 
                NPC.ai[0] = -10; //force rapid jumping
            NPC.AISlime(ref NPC.ai, false, 60, 3f, -2f, 6f, -4f);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.FrameHandler_HostileFrog(frameHeight);
            if (NPC.IsABestiaryIconDummy)
                NPC.alpha = 0;
        }

        public override void PostAI()
        {
            NPC.FadeInOutBasedOnAliveEntities(true, -1, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.GetModPlayer<ZAAPlayer>().ZoneMush && AADowned.downedMushroomMonarch ? .3f : 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Mushium>(), 4));
        }
    }
}