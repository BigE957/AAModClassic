using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._CrossMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Mire.__Hardmode.NPCs._Underground
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Miresquito : ModNPC, IBannerNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Miresquito");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.width = 64;
			NPC.height = 64;
			NPC.damage = 70;
			NPC.defense = 10;
			NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 6000f;
            NPC.lavaImmune = false;
            NPC.knockBackResist = 0.5f;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<MiresquitoBanner>();
            SpawnModBiomes = [ModContent.GetInstance<UndergroundMireBiome>().Type];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && !spawnInfo.Player.ZoneSurface() && spawnInfo.Player.ZoneAnyMire() && !NPCUtils.AnyEvents(spawnInfo.Player))
                return ContentReplacementSystem.NeedToReplaceContent ? 0.25f : .025f;

            return 0f;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 7)
            {
                NPC.frame.Y += 60;
                NPC.frameCounter = 0;
                if (NPC.frame.Y >= 240)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void AI()
        {
            BaseAI.AIFlier(NPC, ref NPC.ai, false, 0.2f, 0.1f, 3, 2.5f, true, 250);
            NPC.rotation = NPC.velocity.X * 0.05f;
            if (NPC.velocity.X > 0)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bogtoxin>(), 1, 1, 2));
        }
    }
}
