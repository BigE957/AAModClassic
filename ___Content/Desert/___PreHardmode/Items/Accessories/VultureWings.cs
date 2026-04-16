using AAModClassic.___Content.Desert.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Desert.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
	public class VultureWings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Vulture Wings");
            // Tooltip.SetDefault("Allows slow fall");

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(30, 4);
        }

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 30;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<VultureFeather>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DesertMana>(), 5);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}