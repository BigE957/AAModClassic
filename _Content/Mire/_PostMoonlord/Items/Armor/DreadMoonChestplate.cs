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
    [AutoloadEquip(EquipType.Body)]
	public class DreadMoonChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DreadMoon";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Dread Moon Gi");
			/* Tooltip.SetDefault(@"'The abyssal wrath of the Mire rests in this armor'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 3000000;
			Item.defense = 44;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Ranged) += .35f;
            AddEffect(new MovementSpeedEffect(0.20f));
            AddEffect(new MaxRunSpeedEffect(0.20f));
            AddEffect(new MaxLifeEffect(50));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DepthChestplate>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}