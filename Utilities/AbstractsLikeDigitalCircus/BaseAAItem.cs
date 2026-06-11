using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus
{
    public abstract class BaseAAItem : ModItem
    {
		public const int GLOWMASKTYPE_NONE = -1;	 //for shit like Daystorm which is a 'projectile' gun
		public const int  GLOWMASKTYPE_SWORD = 0; //for swords and swordlike items
		public const int GLOWMASKTYPE_GUN = 1; //for guns and gunlike items (bows too)
        public int AARarity = 0; //TODO: rework to use tml rarity system

        //glowmask shenanigans
        public static Dictionary<int, Asset<Texture2D>> GlowmaskCache = [];

        public virtual Color GlowmaskDrawColor => Color.White;

        //custom name color
        public Color? customNameColor = null;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (GlowmaskCache.TryGetValue(Item.type, out var asset) && GlowmaskDrawColor != Color.White)
            {
                spriteBatch.Draw
                (
                    asset.Value, 
                    position, 
                    null, 
                    GlowmaskDrawColor, 
                    0, 
                    origin, 
                    scale, 
                    SpriteEffects.None, 
                    0f
                );
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (GlowmaskCache.TryGetValue(Item.type, out var asset))
			{
				spriteBatch.Draw
				(
					asset.Value,
					new Vector2
					(
						Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
						Item.position.Y - Main.screenPosition.Y + Item.height - asset.Value.Height * 0.5f + 2f
					),
					new Rectangle(0, 0, asset.Value.Width, asset.Value.Height),
                    GlowmaskDrawColor,
					rotation,
					asset.Value.Size() * 0.5f,
					scale,
					Item.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
					0f
				);
			}
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (customNameColor != null)
            {
                foreach (TooltipLine line2 in list)
                {
                    if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                    {
                        line2.OverrideColor = (Color)customNameColor;
                    }
                }
                return;
            }

            BaseAAItem AAitem = (BaseAAItem)Item.ModItem;
            if (AAitem.AARarity != 0)
            {
                Color Rare;
                switch (AAitem.AARarity)
                {
                    default: Rare = Color.White; break;
                    case 12: Rare = AAColor.Rarity12; break; //Ashe and Haruka
                    case 13: Rare = AAColor.Rarity13; break; //Ancients
                    case 14: Rare = AAColor.Rarity14; break; //Super Ancients	
                    case 15: Rare = AAColor.Rarity15; break; //Hyper Ancients				
                }
                foreach (TooltipLine line2 in list)
                {
                    if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                    {
                        line2.OverrideColor = Rare;
                    }
                }
            }
        }

		//DO NOT FUCK WITH THIS!! EDITING THIS COULD BREAK ITEMS BADLY!!!
		public override ModItem NewInstance(Item itemClone)
		{
			BaseAAItem newItem = (BaseAAItem)base.NewInstance(itemClone);
			newItem.customNameColor = customNameColor;
            return newItem;
		}
	}
}