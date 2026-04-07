
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic;
using AAModClassic.NPCs.Bosses.Grips;
using AAModClassic.Globals;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;

namespace AAModClassic.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class InterestingClaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Interesting Looking Claw");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"It's oddly Clammy
Can only be used at night"); */
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

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<HydraClaw>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<GripOfChaosBlue>()) || NPC.AnyNPCs(ModContent.NPCType<GripOfChaosRed>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.InterestingClawFalse1"), Color.Indigo, false);
                return false;
            }
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.InterestingClawFalse2"), Color.Indigo, false);
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAWorld.spawnGrips = false;
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAModClassic.Grips.GripsofChaosAwoken")), new Color(175, 75, 255), -1);
            }
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GripOfChaosBlue>(), false, 1, 0);
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GripOfChaosRed>(), false, -1, 0);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}