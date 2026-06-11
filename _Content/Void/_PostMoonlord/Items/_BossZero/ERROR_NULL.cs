
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Content.Void.___PreHardmode.NPCs;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero
{
    //imported from my tAPI mod because I'm lazy
    public class ERROR_NULL : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ERR0R_NULL");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"ACTIVATES THE GR0UND ZER0 C0DE F0R THE NEAREST ZER0 UNIT
N0N-C0NSUMABLE"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 41));
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Oblivion;
                }
            }
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                if (NPC.AnyNPCs(ModContent.NPCType<Zero>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<ZeroA>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitVoidZoneFalse"), new Color(255, 0, 0), false);
            return false;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitRuneTrue"), Color.Red.R, Color.Red.G, Color.Red.B);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAWorld.zeroUS = true;
                if (!NPC.AnyNPCs(ModContent.NPCType<ZeroDeactivated>()))
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X, (int)player.position.Y - 300, ModContent.NPCType<ZeroA>());
            }

            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/ZeroDeath"));
            return true;
        }
    }
}