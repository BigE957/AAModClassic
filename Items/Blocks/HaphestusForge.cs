using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using AAModClassic;
using AAModClassic.Tiles.Crafters;

namespace AAModClassic.Items.Blocks
{
    public class HaphestusForge : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hephaestus Forge");
            /* Tooltip.SetDefault(
@"*Slaps top of forge* This baby can fit so many crafting stations in it
Functions as a Hellforge, Hellstone Anvil, Alchemy Table, Demon Altar, Tinkerer's Workshop, and a Table and Chair"); */
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Pink;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<HaphestusForge_Tile>();
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

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:Altar");
            recipe.AddIngredient(ItemID.Hellforge, 1);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddIngredient(ItemID.TinkerersWorkshop, 1);
            recipe.AddIngredient(ItemID.WoodenTable);
            recipe.AddIngredient(ItemID.WoodenChair, 1);
            recipe.AddIngredient(ModContent.ItemType<HellstoneAnvil>(), 1);
            recipe.Register();
        }
    }
}
