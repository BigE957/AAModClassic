using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.___Content.Inferno._PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Inferno.__Hardmode.Items.Materials
{
    public class RadiantIncineriteBar : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<RadiantIncineriteBar_Tile>();
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Incinerite Bar");
            // Tooltip.SetDefault("You can barely look at it, it's so bright");
        }

		public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 1);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Autohammer);
            recipe.Register();
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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
    }
}
