using AAModClassic;
using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.FishingItem.Crate
{
    public class MireCrate : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 99;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = ModContent.TileType<MireCrate>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Crate");
            // Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            AAModGlobalItem.OpenAACrate(player, 1);
        }
    }
}