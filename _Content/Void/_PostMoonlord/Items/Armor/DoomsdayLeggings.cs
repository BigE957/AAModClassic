using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ModLoader;


namespace AAModClassic._Content.Void._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class DoomsdayLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Doomsday";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomsday Assault Greaves");
			/* Tooltip.SetDefault(@"'The power to destroy entire planets rests in this armor'"); */

		}

        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 18;
			Item.value = 3000000;
			Item.defense = 28;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new MovementSpeedEffect(0.18f));
            AddEffect(new MaxRunSpeedEffect(0.18f));
            AddEffect(new MaxManaEffect(120));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 18);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}