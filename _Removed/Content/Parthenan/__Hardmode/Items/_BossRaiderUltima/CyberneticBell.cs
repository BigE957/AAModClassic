using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima
{
    public class CyberneticBell : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";

        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.

            // DisplayName.SetDefault("Cybernetic Bell");
            /* Tooltip.SetDefault(@"A carefully tinkered bell
Summons the Raider Ultima
Can only be used at night"); */
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 38;
            Item.maxStack = Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<RaiderUltima>(), true, 0, 0, "The Raider Ultima", false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer)
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.CyberneticBellFalse1"), Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<RaiderUltima>()))
            {
                if (player.whoAmI == Main.myPlayer)
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.CyberneticBellFalse2"), Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 6);
            recipe.AddRecipeGroup("AAModClassic:IronBar", 6);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSmite>(), 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}