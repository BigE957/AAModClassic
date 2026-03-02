using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
	public class SlapBone : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.SlapHand);
			Item.damage = 56;
			Item.useTime = 15;
			Item.useAnimation = 15;     
			Item.knockBack = 100;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.Pink;            
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Slap Bone");
			/* Tooltip.SetDefault(@"The smallest smack will send your enemies into orbit!
Slap Hand EX"); */
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();      
			recipe.AddIngredient(ItemID.SlapHand);
			recipe.AddIngredient(Mod.Find<ModItem>("EXSoul").Type);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();

            // rattle rattle
		}
	}
}
