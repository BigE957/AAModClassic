using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class MushiumChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Mushium";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushium Shirt");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 50;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 4;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

        public override void RegisterEquipStats()
        {
			AddEffect(new LifeRegenEffect(2));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MushiumBar>(), 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}