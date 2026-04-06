using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.Blocks.Boxes
{
	public class Equibox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Equinox Worms Music Box");
            // Tooltip.SetDefault(@"Plays 'Orbittal Equilibrium' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Equibox_Tile>();
            Item.width = 72;
			Item.height = 36;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
        }
        
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/EquiBox_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            if (Main.expertMode == true)
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.MusicBox);
                recipe.AddIngredient(null, "DarkEnergy", 5);
                recipe.AddIngredient(null, "Stardust", 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.Register();
            }
        }
    }
}
