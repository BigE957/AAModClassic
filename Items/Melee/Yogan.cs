using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class Yogan : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.Sunfury);

            Item.damage = 48; 
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */; 
            Item.width = 46; 
            Item.height = 66;    
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.shoot = Mod.Find<ModProjectile>("Yogan").Type;
			Item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Yogan");
			// Tooltip.SetDefault(@"Ignites enemies on hit");
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Pyrosphere").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("GlacierBreaker").Type);
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
			recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Pyrosphere").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("GlacierBreaker").Type);
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
