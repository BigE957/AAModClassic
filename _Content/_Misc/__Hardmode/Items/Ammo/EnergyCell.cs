using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Ammo
{
    public class EnergyCell : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.width = 8;
			Item.height = 16;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Pink;
			Item.consumable = true;
			Item.shoot = ModContent.ProjectileType<EnergyCell_Proj>();
			Item.ammo = Item.type;
			
		}
		
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Energy Cell");
		}

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow").Value;
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
