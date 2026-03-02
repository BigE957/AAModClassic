using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class Musharang : BaseAAItem
	{
		public override void SetDefaults()
		{

            Item.damage = 16;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
			Item.useTime = 16;
			Item.useAnimation = 16;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 1;
			Item.shootSpeed = 6f;
			Item.shoot = Mod.Find<ModProjectile>("Musharang").Type;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.noMelee = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Musharang");
            // Tooltip.SetDefault("");
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
