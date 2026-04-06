using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class Seashroom : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Seashroom");

			// Tooltip.SetDefault("It smells like fish");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ModContent.ProjectileType<Projectiles.Sharkron>();
            
            Item.buffType = ModContent.BuffType<Sharkron_Buff>();
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

        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}