using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    [AutoloadEquip(EquipType.Shield)]
    public class BulwarkOfChaos : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 2;
            Item.expert = true; Item.expertOnly = true;
            Item.accessory = true;
            Item.defense = 3;
        }
        public override void SetStaticDefaults()
        {            // DisplayName.SetDefault("Bulwark Of Chaos");
            /* Tooltip.SetDefault(
@"For every hit you land on an enemy, 5 true damage (damage unassigned to any class) is dealt
Allows you to dash into enemies, damaging them"); */
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<AAPlayer>().clawsOfChaos = true;
            player.dash = 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ClawOfChaos", 1);
            recipe.AddIngredient(ItemID.EoCShield, 1);
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