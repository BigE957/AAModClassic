using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Acropolis.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAModClassic._Unreleased.Content.Acropolis.__Hardmode.NPCs.__Athena
{
    public class OlympianDragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Olympian Dragon");
            Main.npcFrameCount[NPC.type] = 5;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new(0, 18)
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 38;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.damage = 30;
            NPC.defense = 30;
            NPC.lifeMax = 150;
            NPC.HitSound = SoundID.DD2_WyvernHurt;
            NPC.DeathSound = SoundID.DD2_WyvernDeath;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.05f;
            NPC.npcSlots = 0f;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            SpawnModBiomes = [ModContent.GetInstance<AcropolisBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky);
        }

        public override bool PreAI()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) &&!NPC.AnyNPCs(ModContent.NPCType<Athena>()) && !NPC.AnyNPCs(ModContent.NPCType<AthenaA>()))
            {
                NPC.velocity *= .95f;
                if (NPC.alpha != 0)
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].noLight = false;
                    }
                }
                NPC.alpha += 3;
                if (NPC.alpha >= 255 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.active = false;
                }
                return false;
            }
            return true;
        }

        public override void AI()
        {
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = false;
                }
            }
            NPC.alpha -= 3;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.15f, 0.08f, 6f, 5f, false, 300);
            Player player = Main.player[NPC.target];
            if (player.Center.X > NPC.Center.X)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !Main.hardMode)
            {
                return 0f;
            }
            return SpawnCondition.Sky.Chance * 0.10f;
        }

    }
}