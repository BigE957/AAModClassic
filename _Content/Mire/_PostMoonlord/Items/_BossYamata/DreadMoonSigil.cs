using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._CrossMod;
using AAModClassic.Base;
using AAModClassic.Globals;
using AAModClassic.UI.Dialogue;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata
{
    public class DreadMoonSigil : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dread Moon Sigil");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"A ragged old tablet said to contain the dark magic of a new moon
Summons Yamata
Can only be used at night in the mire
Non-Consumable"); */
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 500;
            Item.consumable = false;
            Item.rare = ItemRarityID.Red;
        }



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 5);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Vector2 spawnPos = new(player.Center.X, player.Center.Y - 100);
            SpawnBoss(player, ModContent.NPCType<YamataBody>(), true, spawnPos, Language.GetTextValue("Mods.AAModClassic.Common.Yamata"));
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/YamataRoar"), player.Center);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                {
                    if (!AAWorld.downedYamata)
                        WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Yamata.Spawn", player.Center, 5, 30, false, new BossText());
                    else
                        WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Yamata.Spawn", player.Center, 6, 30, false, new BossText());
                }
                else
                {
                    if (!AAWorld.downedYamata)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadSigilTrue1"), AAColor.YamataDialogue);
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadSigilTrue2"), AAColor.YamataDialogue);
                }
            }

            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Yamata.Spawn", Main.LocalPlayer.Center, 0, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadTimeFalse"), AAColor.YamataDialogue, false);
                }
                return false;
            }

            if (!player.ZoneAnyMire())
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Yamata.Spawn", Main.LocalPlayer.Center, 1, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadMireFalse"), AAColor.YamataDialogue, false);
                }
                return false;
            }

            if (!ContentReplacementSystem.NeedToReplaceContent && !AAWorld.downedYamata && !player.GetModPlayer<ZAAPlayer>().ZoneRisingMoonLake)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Yamata.Spawn", Main.LocalPlayer.Center, 2, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadSigilMireFalse"), AAColor.YamataDialogue, false);
                }
                return false;
            }

            bool anyActiveYamata = false;
            bool anyBlockingEnemy = false;
            bool isAwakened = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<YamataABody>())
                {
                    anyActiveYamata = true;
                    isAwakened = true;
                    break;
                }

                if (npc.type == ModContent.NPCType<YamataBody>())
                {
                    anyActiveYamata = true;
                    isAwakened = false;
                    break;
                }

                if (npc.type == ModContent.NPCType<ShenDoragon>() || npc.type == ModContent.NPCType<ShenDoragonA>() ||
                    npc.type == ModContent.NPCType<ShenDoragonSpawn>() || npc.type == ModContent.NPCType<ShenDoragonTransition>() || npc.type == ModContent.NPCType<ShenDoragonDeath>() || npc.type == ModContent.NPCType<ShenDoragonDefeat>() ||
                    npc.type == ModContent.NPCType<YamataTransition>())
                    anyBlockingEnemy = true;
            }

            if (anyActiveYamata)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Yamata.Spawn", Main.LocalPlayer.Center, isAwakened ? 4 : 3, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DreadFalse2"), isAwakened ? new Color(146, 30, 68) : AAColor.YamataDialogue, false);
                }
                return false;
            }

            if (anyBlockingEnemy)
                return false;

            return true;
        }

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
                npcCenter = player.Center;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(bossType))
                    return;
                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;
                if (spawnMessage)
                {
                    string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName;
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].ModNPC != null)
                        npcName = Main.npc[npcID].ModNPC.DisplayName.ToString();
                    if (namePlural)
                    {
                        BaseUtility.Chat(npcName + " " + Language.GetTextValue("Mods.AAModClassic.Common.BosshasAwoken"), 175, 75, 255, false);
                    }
                    else
                    {
                        BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false);
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage<SummonNPCFromClient>((byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }

    }
}