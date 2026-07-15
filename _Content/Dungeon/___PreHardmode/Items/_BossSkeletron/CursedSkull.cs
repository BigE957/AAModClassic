using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ModLoader;

namespace AAModClassic._Content.Dungeon.___PreHardmode.Items._BossSkeletron
{
    public class CursedSkull : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Skull");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons Skeletron
Can only be used at night"); */

            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, NPCID.SkeletronHead, true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.Skeletron"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime && !NPC.AnyNPCs(NPCID.SkeletronHead))
            {
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Bone, 50);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}