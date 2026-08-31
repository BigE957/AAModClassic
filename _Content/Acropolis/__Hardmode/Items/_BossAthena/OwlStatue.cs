using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA;
using AAModClassic._Unofficial.Desert;
using AAModClassic.Effects;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena
{
    public class OwlStatue : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Owl Statue");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons Athena
Can only be used in the Acropolis at the Owl Altar
'It stares into your soul.'"); */
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
                    list[i].Text = Language.GetTextValue("Mods.AAModClassic.Items.BossSummon.OwlStatue.TooltipAlt1");
                    indexToInsert = i + 1;
                    break;
                }
            }

            list.Insert(indexToInsert, new(Mod, "Tooltip0.5", Language.GetTextValue("Mods.AAModClassic.Items.BossSummon.OwlStatue.TooltipAlt3")));
            list.Insert(indexToInsert, new(Mod, "Tooltip0.5", Language.GetTextValue("Mods.AAModClassic.Items.BossSummon.OwlStatue.TooltipAlt2")));

            list.RemoveAt(indexToRemove);
        }

        public override bool CanUseItem(Player player)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return false;

            if (!player.GetModPlayer<ZAAPlayer>().ZoneAcropolis || (!NPCExtensions.BeenKilled<AthenaA>() && !AAWorld.AthenaAwakened))
                return false;

            return !NPC.AnyNPCs(ModContent.NPCType<Athena>()) && !NPC.AnyNPCs(ModContent.NPCType<AthenaA>());
        }

        public override bool? UseItem(Player player)
        {
            Vector2 spawnPos = player.Center - Vector2.UnitY * 128;
            int a = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<AthenaA>());
            int b = Projectile.NewProjectile(NPC.GetBossSpawnSource(player.whoAmI), spawnPos.X, spawnPos.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
            
            CombatText.NewText(Main.npc[a].Hitbox, Color.CadetBlue, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.AwakenedSummon"));
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<SeraphFeather>(), 15);
            recipe.AddIngredient(ItemID.Sapphire, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}