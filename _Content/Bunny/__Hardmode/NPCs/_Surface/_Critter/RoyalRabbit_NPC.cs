using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAModClassic._Content.Bunny.__Hardmode.NPCs._Surface._Critter
{
    public class RoyalRabbit_NPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Royal Rabbit");
            Main.npcFrameCount[NPC.type] = 7;
        }
        public override void SetDefaults()
        {
            NPCID.Sets.TownCritter[NPC.type] = true;
            NPC.width = 28;
            NPC.height = 24;
            NPC.defense = 0;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.npcSlots = 0f;
            NPC.aiStyle = NPCAIStyleID.Passive;
            AIType = NPCID.Bunny;  //npc behavior
            AnimationType = NPCID.Bunny;
            NPC.dontTakeDamageFromHostiles = false;
            Banner = NPC.type;
            BannerItem = ItemID.BunnyBanner;
            NPC.catchItem = (short)ModContent.ItemType<RoyalRabbit_Item>();
            NPC.rarity = 6;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.RoyalRabbit")
            ]);
        }

        public override void OnKill()
        {
            Player player = Main.player[Player.FindClosest(NPC.Center, NPC.width, NPC.height)];
            int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
            if (bunnyKills % 100 == 0 && bunnyKills < 1000)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossSummonsInfo.RoyalRabbit1"), 107, 137, 179);
                SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Rajah"), NPC.Center);
                AAModGlobalNPC.SpawnRajah(player, true, new Vector2(NPC.Center.X, NPC.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));
            }
            if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossSummonsInfo.RoyalRabbit2") + player.name.ToUpper() + "!!!", 107, 137, 179);
                SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Rajah"), NPC.Center);
                AAModGlobalNPC.SpawnRajah(player, true, new Vector2(NPC.Center.X, NPC.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 77, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RoyalRabbit1").Type, 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDayGrassCritter.Chance * (NPC.downedGolemBoss ? .005f : 0f);
        }
        
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
    }
}