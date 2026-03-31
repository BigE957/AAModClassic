using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic;

namespace AAModClassic._Unreleased.Backgrounds
{
    public class CthulhuHandler : ModSystem
    {
		ScreenCthulhuFog CthulhuFog = new ScreenCthulhuFog(false);
		
        public override void PostDrawTiles()
        {
            CthulhuFog.Update(Mod.GetTexture("_Unreleased/Backgrounds/CthulhuClouds"));
            CthulhuFog.Draw(Mod.GetTexture("_Unreleased/Backgrounds/CthulhuClouds"), false, Color.White, true);
        }
    }
}