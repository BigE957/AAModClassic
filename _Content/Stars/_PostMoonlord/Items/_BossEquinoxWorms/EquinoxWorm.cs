using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Daybringer;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Nightcrawler;
using AAModClassic._Content.Stars.World.Altar;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms
{
    public class EquinoxWorm : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Equinox Worm");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"A worm created using celestial materials
Summons the Equinox Worms
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Purple;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>()) && !NPC.AnyNPCs(ModContent.NPCType<DaybringerHead>()) && !NPC.AnyNPCs(ModContent.NPCType<WormSpawn>());
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.EquinoxWormawoken"), 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
                if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.EquinoxWormawoken"), 175, 75, 255, false); }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAModClassic.Common.EquinoxWormawoken")), new Color(175, 75, 255), -1);
                }
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<DaybringerHead>(), false, 0, 0);
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<NightcrawlerHead>(), false, 0, 0);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}