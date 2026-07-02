using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Accessories;
using AAModClassic._Content.Mire._PostMoonlord.Items.Accessories;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
	public class WingsOfChaos : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
        public override void Load()
        {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Wings_Alt", EquipType.Wings, name: "WingsOfChaos_Wings_Alt");
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wings of Chaos");
            // Tooltip.SetDefault("Allows flight and slow fall");

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(250, 16, 3.7f);
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
			Item.accessory = true;
            
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new WingTimeMaxEffect(250));
        }

        public override void UpdateVisibleAccessory(Player player, bool hideVisual)
        {
            int blue = EquipLoader.GetEquipSlot(Mod, "WingsOfChaos", EquipType.Wings);
            int red = EquipLoader.GetEquipSlot(Mod, "WingsOfChaos_Wings_Alt", EquipType.Wings);
            
            if (player.wings == blue && player.direction == -1)
                player.wings = red;
            else if(player.wings == red && player.direction == 1)
                player.wings = blue;
        }

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.95f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 4f;
			constantAscend = 0.135f;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DraconianWings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadMoonWings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}