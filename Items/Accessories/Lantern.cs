using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using AAModClassic.Items.Materials;
using AAModClassic.Globals;

namespace AAModClassic.Items.Accessories
{
    public class Lantern : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Item.accessory)
            {
                player.buffImmune[ModContent.BuffType<Buffs.Clueless_Buff>()] = true;
            }
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
                AAColor.Lantern,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = TextureAssets.Item[Item.type].Value;
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                //Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, AAColor.Lantern, 0, origin, scale, SpriteEffects.None, 0f);

            }

            return false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lantern");
            // Tooltip.SetDefault(@"Permanent accessory to partially remove The Fog");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonClaw>(), 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}