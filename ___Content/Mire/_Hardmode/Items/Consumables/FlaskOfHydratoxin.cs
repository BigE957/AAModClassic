using AAModClassic.___Content.Mire._Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Consumables
{
    public class FlaskOfHydratoxin : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flask of Hydratoxin");
			// Tooltip.SetDefault("Melee attacks inflict hydratoxin");
		}    

		public override void SetDefaults()
		{
			Item.UseSound = SoundID.Item3;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.maxStack = 30;
			Item.consumable = true;
			Item.width = 22;
			Item.height = 28;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;
			Item.buffType = ModContent.BuffType<FlaskOfHydratoxin_Buff>();
			Item.buffTime = 52000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ModContent.ItemType<Bogtoxin>(), 2);
			recipe.AddTile(TileID.ImbuingStation);
			recipe.Register();
		}
	}
}
