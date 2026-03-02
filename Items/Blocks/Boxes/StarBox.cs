using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Blocks.Boxes
{
    public class StarBox : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Celestial Stars Box");
            // Tooltip.SetDefault(@"Plays 'Star's Serenade' by Charlie Debnam");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("StarBox").Type;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000;
            Item.accessory = true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            if (!Main.dayTime)
            {
                texture = Mod.GetTexture("Items/Blocks/Boxes/StarBoxN");
            }
            spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                BaseDrawing.GetLightColor(Item.position),
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            if (!Main.dayTime)
            {
                texture = Mod.GetTexture("Items/Blocks/Boxes/StarBoxN");
            }
            spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "DarkmatterOre", 5);
            recipe.AddIngredient(null, "RadiumOre", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
