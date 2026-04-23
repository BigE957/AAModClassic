using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using ReLogic.Content;

using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;

namespace AAModClassic.Backgrounds
{
    //TODO: look over this. theres like 20 assorted cloud cs files and i hate them
    // also texture isnt used. thats weird? so all clouds are the same tex? that doesnt seem intentional
    public class FogOverlay(string textureName, string shaderName = "Default", EffectPriority priority = EffectPriority.VeryLow, RenderLayers layer = RenderLayers.All) : Overlay(priority, layer)
    {
        private readonly Asset<Texture2D> texture = ModContent.Request<Texture2D>(textureName ?? "");
        private readonly ScreenShaderData shader = new ScreenShaderData(shaderName);

        public int fogOffsetX = 0;
        public float fadeOpacity = 0f;
        public float dayTimeOpacity = 0f;

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (fadeOpacity == 0f) return; //don't draw if no fog
            Main.spriteBatch.Begin();
            Player player = Main.LocalPlayer;
            Texture2D fog = AAMod.GetTexture("Backgrounds/FogTex");

            Color DefaultFog = new Color(62, 68, 100);
            Color YamataFog = new Color(100, 38, 62);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataABody>());

            Color fogColor = GetAlpha(YamataA ? YamataFog : DefaultFog, 0.4f * fadeOpacity * dayTimeOpacity);

            //ensure we cover the whole screen first
            // Main.spriteBatch.Draw(texture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null, bgColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);	

            //overlap a little so you cannot see edges

            int minX = -fog.Width;
            int minY = -fog.Height;
            int maxX = Main.screenWidth + fog.Width;
            int maxY = Main.screenHeight + fog.Height;


            for (int i = minX; i < maxX; i += fog.Width)
            {
                for (int j = minY; j < maxY; j += fog.Height)
                {
                    if (player.position.Y < Main.worldSurface * 16.0)
                    {
                        Main.spriteBatch.Draw(fog, new Rectangle(i + fogOffsetX, j, fog.Width, fog.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                    }
                }
            }
            Main.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.netMode == NetmodeID.Server || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

            Player player = Main.LocalPlayer;

            Texture2D fog = AAMod.GetTexture("Backgrounds/fog");

            bool inMire = Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire;
            if (BasePlayer.HasAccessory(player, AAMod.instance.Find<ModItem>("Lantern").Type, true, false) || AAWorld.downedYamata) inMire = false;

            fogOffsetX += 1;
            if (fogOffsetX >= fog.Width) fogOffsetX = 0;
            if (inMire)
            {
                fadeOpacity += 0.05f;
                if (fadeOpacity > 1f) fadeOpacity = 1f;
            }
            else
            {
                fadeOpacity -= 0.05f;
                if (fadeOpacity < 0f) fadeOpacity = 0f;
            }
            dayTimeOpacity = Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, 0.3f, 1f, 1f, 1f, 1f, 1f, 0.3f) : 0.3f;
            dayTimeOpacity *= Main.dayTime ? 3f : 1f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Main.LocalPlayer.position = position;
            Mode = OverlayMode.FadeIn;
        }

        public override void Deactivate(params object[] args)
        {
            Mode = OverlayMode.FadeOut;
        }

        public override bool IsVisible()
        {
            return shader.CombinedOpacity > 0f;
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