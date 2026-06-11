using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.NPCs.__BossTechnoTruffle;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle
{
    public class CyberneticShroom : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cybernetic Shroom");
            /* Tooltip.SetDefault(@"Summons the Techno Truffle
Can only be used at night"); */
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
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<TechnoTruffle>(), true, 0, 0, "The Techno Truffle", false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("Stop waving that metal mushroom around like a psychopath.", new Color(216, 110, 40), false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<TechnoTruffle>()))
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("The Techno Truffle exists.", new Color(216, 110, 40), false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ConfusingLookingMushroom>());
                recipe.AddRecipeGroup("AAModClassic:Iron", 6);
                recipe.AddIngredient(ModContent.ItemType<SoulOfSmite>(), 3);
                recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 3);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<IntimidatingLookingMushroom>());
                recipe.AddRecipeGroup("AAModClassic:Iron", 6);
                recipe.AddIngredient(ModContent.ItemType<SoulOfSmite>(), 3);
                recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 3);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
        }
    }
}