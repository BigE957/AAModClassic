using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.NPCs.Bosses.Sag;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;


namespace AAModClassic.Items.BossSummons
{
    public class Lifescanner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lifescanner");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons Sagittarius
Can only be used in the Void"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Sag>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.Sagittarius"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.LifescannerFalse"), new Color(216, 60, 0), false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Sag>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.LifescannerFalse"), new Color(216, 60, 0), false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 10);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}