using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class OrnateBand : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 50;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ornate Band");
            // Tooltip.SetDefault("+50 Max Life");
        }

    }
}