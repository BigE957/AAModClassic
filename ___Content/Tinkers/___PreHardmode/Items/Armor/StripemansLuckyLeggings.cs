using AAModClassic.___Content._PLACEHOLDER;
using AAModClassic.Items.Potions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Tinkers.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class StripemansLuckyLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stripeman's Lucky Pants");
			/* Tooltip.SetDefault(@"Get all of the fisher skill effects
When fish swallowed the hook, you can get an extra fish.
Your fishing rod has chance to steal drops from the enemies and npcs
You can use your fishing rod to catch the items on the ground  
You have more chance to get a crate among the extra booty"); */
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

		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().StripeManFish = true;
			player.fishingSkill += 100;
			player.accFishingLine = true;
			player.accTackleBox = true;
			player.sonarPotion = true;
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