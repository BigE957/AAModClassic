
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AAModClassic._Unreleased.Backgrounds
{
    public class ScreenClouds_Unreleased
    {
		public int fogOffsetX = 0;
		public float fadeOpacity = 0f;
		public float dayTimeOpacity = 0f;
		public bool backgroundClouds = false;

		public ScreenClouds_Unreleased(bool bg)
		{
            backgroundClouds = bg;
		}

        public void Update(Texture2D texture)
        {
			if(Main.netMode == 2 || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

			Player player = Main.player[Main.myPlayer];
			bool inStorm = Main.player[Main.myPlayer].GetModPlayer<AAPlayer_Unreleased>().ZoneStorm;
			if(!backgroundClouds) inStorm = false;
			
			fogOffsetX += 1;
			if(fogOffsetX >= texture.Width) fogOffsetX = 0;
			if(inStorm)
			{
				fadeOpacity += 0.05f;
				if(fadeOpacity > 1f) fadeOpacity = 1f;
			}else
			{
				fadeOpacity -= 0.05f;
				if(fadeOpacity < 0f) fadeOpacity = 0f;
			}
			if(backgroundClouds)
			{
				dayTimeOpacity = (BaseUtility.MultiLerp((float)Main.time / 52000f, 0.5f, 1f, 1f, 1f, 1f, 1f, 0.5f));		
				dayTimeOpacity *= 0.7f; //make it fadier as it's in the background
			}else
			{
				dayTimeOpacity = (BaseUtility.MultiLerp((float)Main.time / 52000f, 0.3f, 1f, 1f, 1f, 1f, 1f, 0.3f));
			}
        }

        public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
        {
			if(fadeOpacity == 0f) return; //don't draw if no fog
            if(setSB) Main.spriteBatch.Begin();


            Color bgColor = GetAlpha(defaultColor, 0.2f * fadeOpacity * dayTimeOpacity);
			Color fogColor = GetAlpha(defaultColor, 0.4f * fadeOpacity * dayTimeOpacity);
			
			int minX = -texture.Width;
			int minY = -texture.Height;
			int maxX = Main.screenWidth + texture.Width;
			int maxY = Main.screenHeight + texture.Height;


            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
					Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? -fogOffsetX : fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
            if(setSB) Main.spriteBatch.End();
        }

		public Color GetAlpha(Color newColor, float alph)
		{
			int alpha = 255 - (int)(255 * alph);
			float alphaDiff = (float)(255 - alpha) / 255f;
			int newR = (int)((float)newColor.R * alphaDiff);
			int newG = (int)((float)newColor.G * alphaDiff);
			int newB = (int)((float)newColor.B * alphaDiff);
			int newA = (int)newColor.A - alpha;
			if (newA < 0) newA = 0;
			if (newA > 255) newA = 255;		
			return new Color(newR, newG, newB, newA);
		}		
    }
}
