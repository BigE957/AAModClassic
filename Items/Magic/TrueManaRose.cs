using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class TrueManaRose : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Mana Rose");
            /* Tooltip.SetDefault(@"Pretty in Pink
Right Clicking fires a piercing rose"); */
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 14;
			Item.width = 68;
			Item.height = 60;
			Item.useTime = 13;
			Item.useAnimation = 13;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 100000;
			Item.rare = 8;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("TrueManaShot").Type;
			Item.shootSpeed = 10f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = Mod.Find<ModProjectile>("TrueManaRose").Type;
                Item.damage = 30;
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.knockBack = 1;
            }
            else
            {
                Item.shoot = Mod.Find<ModProjectile>("TrueManaShot").Type;
                Item.damage = 100;
                Item.useTime = 13;
                Item.useAnimation = 13;
                Item.knockBack = 5;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ManaRose", 1);
            recipe.AddIngredient(null, "PlanteraPetal", 10);
            recipe.AddIngredient(Mod, "HeroShards", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}