
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.Broodmother;
using AAModClassic.Items.Usable;
using AAModClassic.Utilities;
using AAModClassic.___Content.Inferno._PreHardmode.Items.Materials;

namespace AAModClassic.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class DragonBell : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Bell");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"An ornately crafted bell
Summons the Broodmother in the Inferno
Only useable during the day"); */
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 38;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Broodmother>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.Broodmother"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DragonBellDayTimeFalse"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                return false;
            }
            if (player.ZoneAnyInferno())
            {
                if (NPC.AnyNPCs(ModContent.NPCType<Broodmother>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DragonBellFalse1"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DragonBellFalse2"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DragonScale>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Sunpowder>(), 30);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}