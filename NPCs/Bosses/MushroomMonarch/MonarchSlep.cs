using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using AAModClassic.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;


namespace AAModClassic.NPCs.Bosses.MushroomMonarch
{
    public class MonarchSlep : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Very Large Mushroom");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.defense = 0;
            NPC.damage = 0;
            NPC.width = 74;
            NPC.height = 70;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            NPC.value = 0;
            NPC.rarity = 1;
        }

        public override bool PreAI()
        {
            NPC.velocity.Y += .1f;
            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ContentReplacementSystem.NeedToReplaceContent)
                return 0f;

            bool biomeCorrect = spawnInfo.Player.ZoneSurface() && spawnInfo.Player.ZoneForest || spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMush;
            if (spawnInfo.PlayerSafe || NPC.AnyNPCs(ModContent.NPCType<MonarchSlep>()) || NPC.AnyNPCs(ModContent.NPCType<MonarchWake>()) || NPC.AnyNPCs(ModContent.NPCType<MushroomMonarch>()))
            {
                return 0f;
            }
            if (biomeCorrect || Main.dayTime)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.001f;
            }
            return 0f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.MushDust>(), hit.HitDirection, -1f, 0, default, 1f);
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && (NPC.CountNPCS(ModContent.NPCType<MonarchWake>()) + NPC.CountNPCS(ModContent.NPCType<MushroomMonarch>())) < 1)
            {
                int id = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MonarchWake>());
                Main.npc[id].position = NPC.position;
            }
            NPC.active = false;
            NPC.life = 0;
        }
    }
}
