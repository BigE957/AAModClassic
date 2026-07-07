using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Mire._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class DreadMoonLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DreadMoon";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dread Moon Hakama");
			/* Tooltip.SetDefault(@"'The abyssal wrath of the Mire rests in this armor'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 3000000;
			Item.defense = 34;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new MovementSpeedEffect(0.50f));
            AddEffect(new MaxRunSpeedEffect(0.50f));
            AddEffect<AmmoCost75Effect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DepthLeggings>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}