using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ModLoader;


namespace AAModClassic._Content.Void._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    [AutoloadEquipGlow(EquipType.Body)]
    public class DoomsdayChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Doomsday";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Doomsday Assault Armor");
			/* Tooltip.SetDefault(@"'The power to destroy entire planets rests in this armor'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 3000000;
			Item.defense = 38;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void RegisterEquipStats()
        {
			AddEffect(new ManaCostMultiplierEffect(0.75f));
			AddEffect(new MaxManaEffect(50));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 20);
			recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
			recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}
}