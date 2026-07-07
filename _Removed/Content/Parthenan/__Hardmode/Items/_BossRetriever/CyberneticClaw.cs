using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRetriever;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever
{
    public class CyberneticClaw : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.

            // DisplayName.SetDefault("Cybernetic Claw");
            /* Tooltip.SetDefault(@"Summons the Retriever
Only useable at night"); */
        }

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 24;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
		}

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Retriever>(), true, 0, 0, "The Retriever", false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("The claw just lays limp in your hand.", Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Retriever>()))
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("The Retriever is still trying to grab you", Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:ChaosClaw", 6);
            recipe.AddRecipeGroup("AAModClassic:IronBar", 6);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 3);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSmite>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}