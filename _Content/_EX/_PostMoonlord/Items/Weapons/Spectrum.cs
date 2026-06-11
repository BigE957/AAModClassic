using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class Spectrum : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spectrum");
            /* Tooltip.SetDefault(@"Focuses a devastating beam of light
Last Prism EX"); */
           
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 150;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 14;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.reuseDelay = 5;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.UseSound = SoundID.Item13;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 0f;
	        Item.value = 1000000;
	        Item.shoot = ModContent.ProjectileType<Spectrum_Holdout>();
	        Item.shootSpeed = 30f;
			Item.rare = ItemRarityID.Cyan;
	    }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LastPrism);
			recipe.AddIngredient(ModContent.ItemType<EXSoul>());
			recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}