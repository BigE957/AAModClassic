using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items.Accessories.Vanity;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs
{
    public class Null : ModNPC, IBannerNPC
    {
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Null");
            Main.npcFrameCount[NPC.type] = 4;
        }
		
		public override void SetDefaults()
		{
            NPC.CloneDefaults(NPCID.Poltergeist);
            NPC.noGravity = true;
            NPC.noTileCollide = true;
			NPC.aiStyle = -1;
            NPC.width = 24;
            NPC.height = 40;
            NPC.damage = 50;
            NPC.defense = 9999999;
            NPC.lifeMax = 100;
            NPC.HitSound = new SoundStyle("AAModClassic/Sounds/Glitch");
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.alpha = 70;
            NPC.value = 7000f;
            NPC.knockBackResist = 0.7f;
            NPC.noGravity = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.NullBanner>();
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.AAPlayer().ZoneVoid && (NPC.downedMoonlord && AAWorld.downedZero) && !NPCUtils.AnyEvents(spawnInfo.Player))
                return 0.005f;

            return 0f;
        }

        public int frameCount = 0;
		public int frameCounter = 0;
		public override void PostAI()
		{
			
			NPC.frame = new Rectangle(0, frameCount * 40, 36, 38);
			NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
			NPC.rotation = NPC.velocity.X * 0.25f;
		}

        public override void AI()
        {
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            for (int m = 0; m < 2; m++)
            {
                BaseAI.AIEye(NPC, ref NPC.ai, false, true, 0.13f, 0.08f, 2f, 1.1f, 1.2f, 1.2f);
                BaseAI.Look(NPC, 1);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UnstableSingularity>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Ono>(), 100));
        }
    }
}