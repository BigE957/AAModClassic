using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Ryusei : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.SolarEruption);

            Item.damage = 70; 
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */; 
            Item.width = 46; 
            Item.height = 66;    
            Item.knockBack = 7;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shoot = Mod.Find<ModProjectile>("Ryusei").Type;
			Item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ryusei");
			// Tooltip.SetDefault(@"Ignites enemies on hit with flames and Dragonfire");
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Yogan").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("HeroShards").Type);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
