using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit;
using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA
{
    public class TenCaratCarrot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ten Carat Carrot");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"The fury of the Raging Rajah can be felt radiating from this ornate carrot...
Non-consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noUseGraphic = true;
            Item.consumable = false;
            Item.UseSound = new SoundStyle("AAModClassic/Sounds/Rajah");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            /*if (WorldTypeSystem.WorldType == AAWorldType.Beta)
                return false;
            */
            return !(NPC.AnyNPCs(ModContent.NPCType<RajahRabbit>()) ||
                NPC.AnyNPCs(ModContent.NPCType<RajahRabbitA>()));
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (!NPCExtensions.BeenKilled<RajahRabbitA>())
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DiamondCarrotRajahText1"), 107, 137, 179);
            }
            else
            {
                string Name;
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    Name = "Terrarians";
                }
                else
                {
                    Name = Main.LocalPlayer.name;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DiamondCarrotRajahText2") + " " + Name + "!", 107, 137, 179);
            }
            int overrideDirection = Main.rand.NextBool(2) ? -1 : 1;
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<RajahRabbitA>(), false, player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, -1200), Language.GetTextValue("Mods.AAModClassic.Common.SupremeRajah"));
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GoldenCarrot>(), 1);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 3);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 3);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 3);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            //recipe.AddCondition(Language.GetText("Mods.AAModClassic.Common.Conditions.ReleaseOrMixed"), () => WorldTypeSystem.WorldType != AAWorldType.Beta);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<PlatinumCarrot>(), 1);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 3);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 3);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 3);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            //recipe.AddCondition(Language.GetText("Mods.AAModClassic.Common.Conditions.ReleaseOrMixed"), () => WorldTypeSystem.WorldType != AAWorldType.Beta);
            recipe.Register();
        }
    }
}