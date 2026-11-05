using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._CrossMod;
using AAModClassic.Base;
using AAModClassic.Globals;
using AAModClassic.Rarities;
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

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma
{
    public class DraconianSunSigil : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Draconian Sun Sigil");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"An ornate tablet said to contain the radiant power of a thousand suns
Summons Akuma
Only Usable during the day in the inferno
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 0, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianDayTimeFalse"), new Color(180, 41, 32), false);
                }
                return false;
            }
            if (!player.ZoneAnyInferno())
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 6, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSigilInfernoFalse"), new Color(180, 41, 32), false);
                }
                return false;
            }

            if (!ContentReplacementSystem.NeedToReplaceContent && !AAWorld.downedAkuma && !player.GetModPlayer<ZAAPlayer>().ZoneRisingSunPagoda)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 7, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSigilInfernoFalse2"), new Color(180, 41, 32), false);
                }
                return false;
            }

            bool anyActiveAkuma = false;
            bool anyBlockingEnemy = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<AkumaHead>() || npc.type == ModContent.NPCType<AkumaAHead>())
                {
                    anyActiveAkuma = true;
                    break;
                }

                if (npc.type == ModContent.NPCType<ShenDoragon>() || npc.type == ModContent.NPCType<ShenDoragonA>() ||
                    npc.type == ModContent.NPCType<ShenDoragonSpawn>() || npc.type == ModContent.NPCType<ShenDoragonTransition>() || npc.type == ModContent.NPCType<ShenDoragonDeath>() || npc.type == ModContent.NPCType<ShenDoragonDefeat>() ||
                    npc.type == ModContent.NPCType<AkumaTransition>())
                    anyBlockingEnemy = true;
            }

            if (anyActiveAkuma)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 5, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSigilFalse"), new Color(180, 41, 32), false);
                }
                return false;
            }
            if (anyBlockingEnemy)
                return false;

            return true;

        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!AAWorld.downedAkuma)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 8, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSignalTrue1"), new Color(180, 41, 32));
                }
                else
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Akuma.Spawn", Main.LocalPlayer.Center, 9, 60, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSignalTrue2"), new Color(180, 41, 32));
                }
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AkumaHead>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.Akuma"), false);
            }
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/AkumaRoar"), player.position);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 5);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}