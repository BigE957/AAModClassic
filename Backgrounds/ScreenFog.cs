using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.NPCs.Bosses.Yamata.Awakened;

namespace AAModClassic.Backgrounds
{
    public class ScreenFog
    {
		public int fogOffsetX = 0;
		public float fadeOpacity = 0f;
		public float dayTimeOpacity = 0f;
		public bool backgroundFog = false;

		public ScreenFog(bool bg)
		{
			backgroundFog = bg;
		}

        public void Update(Texture2D texture)
        {
			if(Main.netMode == NetmodeID.Server || Main.dedServ || Main.gameMenu) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

			Player player = Main.LocalPlayer;
			bool inMire = Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire;
			if(!backgroundFog && (BasePlayer.HasAccessory(player, AAMod.instance.Find<ModItem>("Lantern").Type, true, false) || AAWorld.downedYamata)) inMire = false;
			
			fogOffsetX += 1;
			if(fogOffsetX >= texture.Width) fogOffsetX = 0;
			if(inMire)
			{
				fadeOpacity += 0.05f;
				if(fadeOpacity > 1f) fadeOpacity = 1f;
			}else
			{
				fadeOpacity -= 0.05f;
				if(fadeOpacity < 0f) fadeOpacity = 0f;
			}
			if(!backgroundFog)
			{
				dayTimeOpacity = Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, 0.5f, 1f, 1f, 1f, 1f, 1f, 0.5f) : 0.5f;		
				dayTimeOpacity *= 0.7f; //make it fadier as it's in the background
			}else
			{
				dayTimeOpacity = Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, 1f, 1f, 1f, 1f, 1f, 1f, 1f) : 0.3f;
                dayTimeOpacity *= Main.dayTime ? 3f : 1f;
			}
        }

        public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
        {
			if(fadeOpacity == 0f) return; //don't draw if no fog
            if(setSB) Main.spriteBatch.Begin();
            Player player = Main.LocalPlayer;

            Color DefaultFog = new Color(62, 68, 100);
            Color YamataFog = new Color(100, 38, 62);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

			Color fogColor = GetAlpha(YamataA ? YamataFog : DefaultFog, 0.4f * fadeOpacity * dayTimeOpacity);

            //ensure we cover the whole screen first
            // Main.spriteBatch.Draw(texture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null, bgColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);	

            //overlap a little so you cannot see edges

            int minX = -texture.Width;
			int minY = -texture.Height;
			int maxX = Main.screenWidth + texture.Width;
			int maxY = Main.screenHeight + texture.Height;


            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
                    if (player.position.Y < Main.worldSurface * 16.0)
                    {
                        Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? -fogOffsetX : fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                    }
                }
            }
            if(setSB) Main.spriteBatch.End();
        }

		public static Color GetAlpha(Color newColor, float alph)
		{
			int alpha = 255 - (int)(255 * alph);
			float alphaDiff = (255 - alpha) / 255f;
			int newR = (int)(newColor.R * alphaDiff);
			int newG = (int)(newColor.G * alphaDiff);
			int newB = (int)(newColor.B * alphaDiff);
			int newA = newColor.A - alpha;
			if (newA < 0) newA = 0;
			if (newA > 255) newA = 255;		
			return new Color(newR, newG, newB, newA);
		}		
    }
}
