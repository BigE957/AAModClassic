using AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class StormRiot : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 18, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.expert = true; Item.expertOnly = true;
            Item.accessory = true;
            Item.defense = 6;
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Riot Shield");
            /* Tooltip.SetDefault(
@"For every hit you land on an enemy, 45 true damage (damage unassigned to any class) is dealt
Allows you to dash into enemies, damaging them
Non-autoswing weapons can be swung faster"); */
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<AAPlayer>().clawsOfChaos = true;
            player.GetModPlayer<StormClawPlayer>().StormClaw = true;
            player.dash = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BulwarkOfChaos>());
            recipe.AddIngredient(ModContent.ItemType<StormClaw>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<StormClaw>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<ClawOfChaos>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}