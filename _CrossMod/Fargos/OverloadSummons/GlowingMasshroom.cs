using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._CrossMod.Fargos.OverloadSummons
{
    public class GlowingMasshroom : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
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
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.GlowingMassShroomInitiated"), Color.SkyBlue, false); 

            for (int i = 0; i < 10; i++)
            {
                NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), ModContent.NPCType<FeudalFungus>());
            }

            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem)
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.GlowingMassShroomFalse"),  Color.SkyBlue, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<FeudalFungus>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem)
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ConfusingMushroomFalse2"), Color.SkyBlue, false);
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
                recipe.AddIngredient(ModContent.ItemType<ConfusingLookingMushroom>(), 1);
                recipe.AddIngredient(fargos, "Overloader", 1);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}