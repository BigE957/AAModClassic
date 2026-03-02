using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.MushroomMonarch;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.BossSummons.Swarm
{
    public class GlowingMasshroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Masshroom");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons a lot of Feudal Fungi
Can only be used in glowing mushroom biomes"); */
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
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat("You are being assaulted by a bunch of fungis", Color.SkyBlue, false);

            for (int i = 0; i < 10; i++)
            {
                NPC.NewNPC((int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), Mod.Find<ModNPC>("Feudal Fungus").Type);
            }

            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat("Stop waving a bunch of shrooms around in the middle of nowhere like a nutcase.",  Color.SkyBlue, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<MushroomMonarch>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ConfusingMushroomFalse2"), Color.SkyBlue, false);
                return false;
            }
            return true;
        }

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
        {
            return ModLoader.GetMod("Fargowiltas") != null;
        }

        private readonly Mod fargos = ModLoader.GetMod("Fargowiltas");

        public override void AddRecipes()
        {
            if (fargos != null)
            {
                Recipe recipe = CreateRecipe(1);
                recipe.AddIngredient(ModContent.ItemType<ConfusingMushroom>(), 1);
                recipe.AddIngredient(fargos, "Overloader", 1);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}