using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Underground.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class StripemansLuckyChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.StripemansLucky";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stripeman's Lucky Shirt");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 50;
			Item.rare = ItemRarityID.Gray;
			Item.defense = 1;
            Item.value = Item.sellPrice(0, 0, 0, 1);
        }

        public override void RegisterInventoryEffects()
        {
            AddEffect<PDAEffect>();
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<PDAEffect>();
            AddEffect<ArcticDivingGearEffect>();
            AddEffect(new AncientGoldLeggingsEffect(true));
            AddEffect<AncientGoldChestplateEffect>();
            AddEffect<AncientGoldChestplateSetEffect>();
            AddEffect<StripemansLuckyChestplateEffect>();
        }

        public override void RegisterVanityEffects()
        {
			AddEffect<PDAEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AncientGoldHelmet, 1);
			recipe.AddIngredient(ModContent.ItemType<AncientGoldChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AncientGoldLeggings>(), 1);
			recipe.AddIngredient(ItemID.ArcticDivingGear, 1);
			recipe.AddIngredient(ItemID.PDA, 1);
			recipe.AddIngredient(ModContent.ItemType<LuckyCracker>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}
	}
}