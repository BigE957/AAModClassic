using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.___PreHardmode.Items.Consumables
{
    public class Moonpowder : BaseAAItem
	{
		public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<Projectiles.Moonpowder>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 4f;
            Item.width = 16;
            Item.height = 24;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noMelee = true;
            Item.value = 75;
        }

		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault(@"Cleanses the Inferno");
		}
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ModContent.ItemType<Darkshroom>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
