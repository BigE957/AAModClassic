using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;
using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class StripemansLuckyLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.StripemansLucky";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stripeman's Lucky Pants");
			ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;
        }

		public override void SetDefaults()
		{
            Item.width = 22;
			Item.height = 18;
			Item.rare = ItemRarityID.Gray;
			Item.defense = 1;
            Item.value = Item.sellPrice(0, 0, 0, 1);
        }

        public override void RegisterEquipEffects()
        {
			AddEffect(new FishingPowerEffect(100));
			AddEffect<HighTestFishingLineEffect>();
            AddEffect<TackleBoxEffect>();
            AddEffect<SonarEffect>();
            //TODO: 1.4.4 added a set bonus for the angler set. make this inherit it in unofficial
            AddEffect<StripemansLuckyLeggingsEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AnglerHat, 1);
			recipe.AddIngredient(ItemID.AnglerVest, 1);
			recipe.AddIngredient(ItemID.AnglerPants, 1);
			recipe.AddIngredient(ItemID.AnglerTackleBag, 1);
			recipe.AddIngredient(ModContent.ItemType<ShinyCharmFish>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LuckyCracker>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}
	}
}