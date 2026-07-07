using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons
{
    public class ManaBlade : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Magic";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mana Blade");
            // Tooltip.SetDefault("Fires Homing projectiles at the cost of mana");
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 5;
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 30;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<ManaBlade_ManaStar>();
			Item.shootSpeed = 8f;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ManaCrystal, 5);
			recipe.AddRecipeGroup("AAModClassic:SilverBar", 15);
			recipe.AddTile(TileID.Anvils);
			recipe.Register(); 
		}
        
	}
}
