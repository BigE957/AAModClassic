
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos
{
    //imported from my tAPI mod because I'm lazy
    public class CuriousLookingClaw : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Curious Looking Claw");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"It's strangely dry
Can only be used at night"); */
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonClaw_Item>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<GripOfChaosMire>()) || NPC.AnyNPCs(ModContent.NPCType<GripOfChaosInferno>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.CuriousClawFalse1"), Color.DarkOrange, false);
                return false;
            }
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.CuriousClawFalse2"), Color.DarkOrange, false);
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AADowned.spawnGrips = false;
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAModClassic.Common.Grips.GripsofChaosAwoken")), new Color(175, 75, 255), -1);
            }
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GripOfChaosMire>(), false, 1, 0);
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GripOfChaosInferno>(), false, -1, 0);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}