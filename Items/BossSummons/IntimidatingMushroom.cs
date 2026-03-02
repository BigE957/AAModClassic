using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.MushroomMonarch;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.BossSummons
{
    public class IntimidatingMushroom : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Intimidating Looking Mushroom");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            // Tooltip.SetDefault(@"Summons the Mushroom Monarch");
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
            AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("MushroomMonarch").Type, true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.MushroomMonarch"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<MushroomMonarch>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.IntimidatingMushroomFalse2"), new Color(216, 110, 40), false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Mushroom, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}