using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee   //where is located
{
    public class GlacierBreaker : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.SolarEruption);

            Item.damage = 18;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 32;              
            Item.height = 46;             

            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;   
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<GlacierBreaker>();
			Item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glacier Breaker");
			// Tooltip.SetDefault(@"Drops Icicles while the flail travels");
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.BorealWood, 20);
			recipe.AddIngredient(ItemID.IceBlock, 40);
			recipe.AddIngredient(ModContent.ItemType<SnowMana>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}
