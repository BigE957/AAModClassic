using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic.Globals;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.NPCs
{
    public class MushroomCrab : ModNPC, IBannerNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Crab");
            Main.npcFrameCount[NPC.type] = 5;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = -2
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

		public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 34;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.damage = 16;
            NPC.defense = 20;
            NPC.lifeMax = 140;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            AnimationType = NPCID.AnomuraFungus;
            NPC.knockBackResist = 0.3f;
            NPC.value = 1300f;
            NPC.buffImmune[31] = false;
            NPC.npcSlots = 0.3f;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.MushroomCrabBanner>();
            SpawnModBiomes = [ModContent.GetInstance<RedMushroomBiome>().Type];
        }

        public override void AI()
        {
            AAAI.InfernoFighterAI(NPC, ref NPC.ai, true, false, -1, 0.13f, 3f, 3, 4, 60, true, 10, 60, true, null, false);
        }

        public override void HitEffect(NPC.HitInfo hit)
		{

            int dust1 = ModContent.DustType<Dusts.MushDust>();
            if (NPC.life <= 0)
			{
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
            }
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.GetModPlayer<ZAAPlayer>().ZoneMush ? .4f : 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Mushroom));
        }
    }
}