using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.NPCs.Bosses.MushroomMonarch;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic;
using AAModClassic.Items.BossSummons;


namespace AAModClassic.Items.BossSummons.Swarm
{
    public class Masshroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Masshroom");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            // Tooltip.SetDefault(@"Summons a lot of Mushroom Monarchs");
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
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("It's time for a Mush Pit", new Color(216, 110, 40), false);
            for (int i = 0; i < 10; i++)
            {
                 NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), Mod.Find<ModNPC>("MushroomMonarch").Type);
            }

            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<MushroomMonarch>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.IntimidatingMushroomFalse2"), new Color(216, 110, 40), false);
                return false;
            }
            return true;
        }

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.TryGetMod("Fargowiltas", out _);
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("Fargowiltas", out var fargos))
            {
                Recipe recipe = CreateRecipe(1);
                recipe.AddIngredient(ModContent.ItemType<IntimidatingMushroom>(), 1);
                recipe.AddIngredient(fargos, "Overloader", 1);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}