using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;


namespace AAModClassic._CrossMod.Fargos.OverloadSummons
{
    public class Masshroom : CrossoverItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";

        public override string CrossoverModName => "Fargowiltas";

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
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem)
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.MassShroomInitiated"), new Color(216, 110, 40), false);
            for (int i = 0; i < 10; i++)
            {
                 NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), ModContent.NPCType<MushroomMonarch>());
            }

            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<MushroomMonarch>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem)
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.IntimidatingMushroomFalse2"), new Color(216, 110, 40), false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<IntimidatingLookingMushroom>(), 1);
            recipe.AddIngredient(ModLoader.GetMod("Fargowiltas"), "Overloader", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}