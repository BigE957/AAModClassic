using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic
{
    public abstract class BaseAAItem : ModItem
    {
		public const int GLOWMASKTYPE_NONE = -1;	 //for shit like Daystorm which is a 'projectile' gun
		public const int  GLOWMASKTYPE_SWORD = 0; //for swords and swordlike items
		public const int GLOWMASKTYPE_GUN = 1; //for guns and gunlike items (bows too)
        public int AARarity = 0;

        //glowmask shenanigans
        public static Dictionary<int, Asset<Texture2D>> GlowmaskCache = [];
        public Color GlowmaskDrawColorALSOREPLACELATER = Color.White;

        public string glowmaskTexture = null;
        public int glowmaskDrawType = 0; //TODO: remove this? does nothing
        // ok so i looked around. its SUPPOSED to do something. but it never does. lol. add that functionality in its place? 
        // even if we add the functionality we can remove this bcuz theres other ways to check usestyle its called checking what the usestyle is
		public Color glowmaskDrawColor = Color.White;

        //custom name color
        public Color? customNameColor = null;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (GlowmaskCache[Item.type] != null && GlowmaskDrawColorALSOREPLACELATER != Color.White)
            {
                spriteBatch.Draw
                (
                    GlowmaskCache[Item.type].Value, 
                    position, 
                    null, 
                    GlowmaskDrawColorALSOREPLACELATER, 
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
			if(glowmaskTexture != null)
			{
	            Texture2D texture = Mod.GetTexture(glowmaskTexture);
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
            newItem.glowmaskTexture = glowmaskTexture;
			newItem.glowmaskDrawType = glowmaskDrawType;
			newItem.glowmaskDrawColor = glowmaskDrawColor;
			newItem.customNameColor = customNameColor;
            return newItem;
		}

        public override void Load()
        {
            if (ModContent.RequestIfExists<Asset<Texture2D>>(Texture + "_Glow", out var texture))
            {
                if (GlowmaskCache.TryAdd(Type, texture.Value) == false)
                {
                    Mod.Logger.Warn("some shit did NOT get loaded into the glowmask cache bcuz something was already there.");
                    Mod.Logger.Warn("item id: " + Type);
                    Mod.Logger.Warn("item name: " + Name);
                    Mod.Logger.Warn("glowmask in that slot: " + texture.Name);
                }
                Mod.Logger.Warn("added item name: " + Name);
            }
        }
	}
}