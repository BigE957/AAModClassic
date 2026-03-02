using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class DragonsGuard : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.accessory = true;
            Item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().DragonsGuard = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Guard");
            // Tooltip.SetDefault(@"Enemies that strike you are set ablaze");
        }
    }
}
