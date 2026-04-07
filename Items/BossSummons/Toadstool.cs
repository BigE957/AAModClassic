using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.NPCs.Bosses.Toad;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Items.BossSummons
{
    public class Toadstool : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toadstool");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons the Truffle Toad
Can only be used in a glowing mushroom biome"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.maxStack = 20;
            Item.value = 1000;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<TruffleToad>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.TruffleToad"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            //if (WorldTypeSystem.WorldType == AAWorldType.Beta)
            //    return false;

            if (!player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ToadstoolFalse1"), Color.Blue, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<TruffleToad>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ToadstoolFalse2"), Color.Blue, false);
                return false;
            }
            return true;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(player, Item, true, true); }
        public override void UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, Item); }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<Mushium>(), 10);
            recipe.AddIngredient(ModContent.ItemType<GlowingMushium>(), 10);
            recipe.AddTile(TileID.Anvils);
            //recipe.AddCondition(Language.GetText("Mods.AAModClassic.Common.Conditions.ReleaseOrMixed"), () => WorldTypeSystem.WorldType != AAWorldType.Beta);
            recipe.Register();
        }
    }
}