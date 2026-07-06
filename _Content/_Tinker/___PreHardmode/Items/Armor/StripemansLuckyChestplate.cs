using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
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
            /* Tooltip.SetDefault(@"Displays everything
You have chance to get gold coins in stoneblocks
You have more chance to meet with rare creatures.
You have more chance to get better things in pots
If you have enough money, you can resist an attack by losting all your money.
Have the effect of Arctic Diving Gear"); */
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