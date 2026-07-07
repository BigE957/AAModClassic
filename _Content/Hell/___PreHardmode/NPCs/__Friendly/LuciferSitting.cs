using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.Hell.___PreHardmode.NPCs.__Friendly
{
    public class LuciferSitting : ModNPC, ILocalizedModType
    {
        public new string LocalizationCategory => "NPCs.TownNPCs";

        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.townNPC = true;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.width = 56;
            NPC.height = 82;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lucifer the Pit Lord");
            Main.npcFrameCount[Type] = 9;
            NPCID.Sets.TownCritter[Type] = true;
            NPCID.Sets.NoTownNPCHappiness[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Lucifer")
            ]);
        }

        public override bool UsesPartyHat() { return false; }

        public override void AI()
        {
            NPC.wet = false;
            NPC.lavaWet = false;
            NPC.honeyWet = false;
            NPC.velocity.X = NPC.velocity.Y = 0f;
            NPC.dontTakeDamage = true;
            NPC.immune[255] = 30;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.homeless = false;
                NPC.homeTileX = -1;
                NPC.homeTileY = -1;
                NPC.netUpdate = true;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 8)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
                if (NPC.frame.Y > frameHeight * 8)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        int chatNumber = 0;

        public override void ResetEffects()
        {
            //chatNumber = 0;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            string who = "Who are you?";
            string when = "When will it be done?";
            string why = "Why are you building it?";
            string bye = "Alright, goodbye.";
            if (chatNumber == 0)
            {
                button = who;
            }
            else if (chatNumber == 1)
            {
                button = when;
            }
            else if (chatNumber == 2)
            {
                button = why;
            }
            else if (chatNumber == 3)
            {
                button = bye;
            }
            else
            {
                button = "";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                if (chatNumber == 0)
                {
                    Main.npcChatText = @"Who am I?! I'm-- Who am I kiddin'. You know who I am. Now skedaddle, I got an arena to get built.";
                }
                else if (chatNumber == 1)
                {
                    Main.npcChatText = @"I don't know, whenever my guys get off their lazy behinds and actually start building stuff.";
                }
                else if (chatNumber == 2)
                {
                    Main.npcChatText = @"You have a lot of questions, don't you? I'm building it because I want to watch guts spill. Why else?";
                }
                else if (chatNumber == 3)
                {
                    Main.npcChatText = @"See you around. Come back when I finish, I'd love to see you get gored! BWAHAHAHAHAHAHAHAHAH!!!";
                }
                chatNumber++;
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }

        public override string GetChat()
        {
            chatNumber = 0;

            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add(@"Come back later. I'm setting up shop here.

Huh? What am I doin'?! I'm supervising.");
            return chat;
        }
    }
}