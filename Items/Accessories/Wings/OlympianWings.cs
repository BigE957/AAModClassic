using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class OlympianWings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Olympian Wings");
            /* Tooltip.SetDefault(@"Allows flight and slow fall
Grants a dash while flying"); */
        }

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = 8;
			Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().AADash = 1;
            player.wingTimeMax = 170;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
                ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2.1f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 8f;
            acceleration *= 2f;
        }
	}
}