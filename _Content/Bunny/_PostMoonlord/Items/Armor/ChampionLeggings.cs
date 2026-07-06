using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChampionLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Champion";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Champion' Greaves");
            /* Tooltip.SetDefault(@"'The armor of a champion feared across the land'"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.defense = 30;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Generic) += .15f;
            AddEffect(new MovementSpeedEffect(0.50f));
            AddEffect(new MaxRunSpeedEffect(0.50f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HoppingHoodlumLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChampionPlate>(), 10);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}