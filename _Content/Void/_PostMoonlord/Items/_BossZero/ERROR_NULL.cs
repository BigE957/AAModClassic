
using AAModClassic._Content.Void.___PreHardmode.NPCs;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic.Base;
using AAModClassic.Globals;
using AAModClassic.UI.Dialogue;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero
{
    //imported from my tAPI mod because I'm lazy
    public class ERROR_NULL : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ERR0R_NULL");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"ACTIVATES THE GR0UND ZER0 C0DE F0R THE NEAREST ZER0 UNIT
N0N-C0NSUMABLE"); */
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 41));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
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

        public override bool CanUseItem(Player player)
        {
            if (!player.GetModPlayer<ZAAPlayer>().ZoneVoid)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Zero.Spawn", player.Center, 0, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitVoidZoneFalse"), Color.Red);
                }
                return false;
            }

            bool anyZeros = false;
            foreach(NPC n in Main.ActiveNPCs)
            {
                if(n.type == ModContent.NPCType<Zero>() || n.type == ModContent.NPCType<ZeroA>())
                {
                    anyZeros = true;
                    break;
                }
            }

            if (anyZeros)
            {
                if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                {
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        WorldTextSystem.StartDialogueOnClient("Mods.AAModClassic.WorldText.Zero.Spawn", player.Center, 1, 30, false, new BossText());
                    else
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitFalse"), Color.Red);
                }
                return false;
            }

            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Zero.Spawn", player.Center, 4, 30, false, new BossText());
                else
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitRuneTrue"), Color.Red);
                AAWorld.zeroUS = true;
                if (!NPC.AnyNPCs(ModContent.NPCType<ZeroDeactivated>()))
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X, (int)player.position.Y - 300, ModContent.NPCType<ZeroA>());
            }

            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/ZeroDeath"));
            return true;
        }
    }
}