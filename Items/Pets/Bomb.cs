using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class Bomb : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Paper Bomb");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.DD2PetGhost);
			Item.shoot = Mod.Find<ModProjectile>("BoomBoi").Type;
            
            Item.buffType = Mod.Find<ModBuff>("BoomBoi").Type;
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
            recipe.AddIngredient(ItemID.StickyBomb, 1);
            recipe.AddIngredient(ItemID.PixieDust, 20);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}