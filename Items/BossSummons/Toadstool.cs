using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

using AAMod.NPCs.Bosses.Toad;

namespace AAMod.Items.BossSummons
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
            Item.rare = 1;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = 4;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("TruffleToad").Type, true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.TruffleToad"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ToadstoolFalse1"), Color.Blue, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<TruffleToad>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ToadstoolFalse2"), Color.Blue, false);
                return false;
            }
            return true;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(p, Item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, Item); return true; }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "Mushium", 10);
            recipe.AddIngredient(null, "GlowingMushium", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}