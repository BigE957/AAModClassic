using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed
{
    public class GoldenGrub : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Golden Grub");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons Greed
Can only be used in Greed's Hoard at the Altar of Desire
'It's really shiny.'"); */
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) || (!AADowned.downedAthenaA && !AADowned.AthenaAwakened))
                return;

            int indexToInsert = -1;
            int indexToRemove = -1;
            for (int i = 0; i < list.Count; i++)
            {
                var line = list[i];
                if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    indexToRemove = i;

                if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                {
                    list[i].Text = Language.GetTextValue("Mods.AAModClassic.Items.BossSummon.GoldenGrub.TooltipAlt1");
                    indexToInsert = i + 1;
                    break;
                }
            }

            list.Insert(indexToInsert, new(Mod, "Tooltip0.5", Language.GetTextValue("Mods.AAModClassic.Items.BossSummon.GoldenGrub.TooltipAlt3")));
            list.Insert(indexToInsert, new(Mod, "Tooltip0.5", Language.GetTextValue("Mods.AAModClassic.Items.BossSummon.GoldenGrub.TooltipAlt2")));

            list.RemoveAt(indexToRemove);
        }

        public override bool CanUseItem(Player player)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return false;

            if (!player.GetModPlayer<ZAAPlayer>().ZoneHoard || (!NPCExtensions.BeenKilled<GreedAHead>() && !AAWorld.GreedAwakened))
                return false;

            return !NPC.AnyNPCs(ModContent.NPCType<GreedHead>()) && !NPC.AnyNPCs(ModContent.NPCType<GreedAHead>());
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnPos = player.Center + Vector2.UnitY * 600;
                AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GreedAHead>(), true, spawnPos, ModContent.GetInstance<GreedAHead>().DisplayName.Value, false);
                Projectile.NewProjectile(NPC.GetBossSpawnSource(player.whoAmI), spawnPos.X, spawnPos.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);

                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Greed.AwakenedSummon"), Color.Goldenrod);
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<CovetiteCrystal>(), 15);
            recipe.AddIngredient(ItemID.Topaz, 2);
            recipe.AddIngredient(ItemID.MechanicalWorm, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}