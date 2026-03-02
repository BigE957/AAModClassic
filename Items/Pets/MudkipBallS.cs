using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class MudkipBallS : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Shiny Fish Ball");

			// Tooltip.SetDefault("It seems to have something in it already");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.UnluckyYarn);
			Item.shoot = Mod.Find<ModProjectile>("MudkipS").Type;
            
            Item.buffType = Mod.Find<ModBuff>("MudkipS").Type;
		}

        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MudkipBall", 1);
            recipe.AddIngredient(null, "ShinyCharm", 1);
            recipe.Register();
        }
    }
}