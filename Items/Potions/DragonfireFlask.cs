using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class DragonfireFlask : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flask of Dragonfire");
			// Tooltip.SetDefault("Melee attacks inflict Dragonflame");
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
			Item.height = 30;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;
			Item.buffType = Mod.Find<ModBuff>("DragonfireFlaskBuff").Type;
			Item.buffTime = 52000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(Mod.Find<ModItem>("DragonFire").Type, 2);
			recipe.AddTile(TileID.ImbuingStation);
			recipe.Register();
		}
	}
}
