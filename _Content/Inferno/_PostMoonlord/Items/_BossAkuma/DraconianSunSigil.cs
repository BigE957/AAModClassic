using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.CrossMod;
using AAModClassic.Utilities;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma
{
    public class DraconianSunSigil : BaseAAItem
    {
        
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
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianDayTimeFalse"), new Color(180, 41, 32), false);
                return false;
            }
            if (player.ZoneAnyInferno())
            {
                if (!ContentReplacementSystem.NeedToReplaceContent && !AAWorld.downedAkuma && !player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianRuneFalse2"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaHead>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSigilFalse"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaAHead>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSigilFalse"), new Color(0, 191, 255), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<ShenDoragon>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonA>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonSpawn>()) ||
                    NPC.AnyNPCs(ModContent.NPCType<ShenDoragonTransition>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonDeath>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDoragonDefeat>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaTransition>()))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSigilInfernoFalse"), new Color(180, 41, 32), false);
            return false;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {

            if (!AAWorld.downedAkuma)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSignalTrue1"), new Color(180, 41, 32));
            }
            if (AAWorld.downedAkuma)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DraconianSignalTrue2"), new Color(180, 41, 32));
            }
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AkumaHead>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.Akuma"), false);
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/AkumaRoar"), player.position);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
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