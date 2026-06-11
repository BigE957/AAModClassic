using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Ammo
{
    public class RadiumArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Radium Arrow");
		}

		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.knockBack = 4f;
			Item.value = 30;
			Item.rare = ItemRarityID.Purple;
			Item.shoot = ModContent.ProjectileType<RadiumArrow_Proj>();
			Item.shootSpeed = 6f; 
			Item.ammo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
		}

		

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(400);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 3);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}
	}
}
