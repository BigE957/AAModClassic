using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class MushiumLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Mushium";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushium Pants");
        }

		public override void SetDefaults()
		{
            Item.width = 22;
			Item.height = 18;
			Item.value = 50;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 3;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new LifeRegenEffect(1));
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