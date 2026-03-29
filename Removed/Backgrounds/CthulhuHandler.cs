using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic;

namespace AAModClassic.Removed.Backgrounds
{
    public class CthulhuHandler : ModSystem
    {
		ScreenCthulhuFog CthulhuFog = new ScreenCthulhuFog(false);
		
        public override void PostDrawTiles()
        {
            CthulhuFog.Update(Mod.GetTexture("Removed/Backgrounds/CthulhuClouds"));
            CthulhuFog.Draw(Mod.GetTexture("Removed/Backgrounds/CthulhuClouds"), false, Color.White, true);
        }
    }
}