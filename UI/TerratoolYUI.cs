using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI
{
    internal sealed class TerratoolYUI : TerratoolUI
    {
        public static int Pick = 300;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages => AAMod.instance.GetTexture("UI/Tools/ToolUIY");

        public override Texture2D ButtonOnImage => AAMod.instance.GetTexture("UI/Tools/ToolButtonY"); 

        public override Texture2D ButtonOffImage => AAMod.instance.GetTexture("UI/Tools/ToolButtonYOff"); 

        public override UIState State => AAMod.instance.TerratoolYState;
		
        public override int HeldItemType => AAMod.instance.Find<ModItem>("YamataTerratool").Type;

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);
            Pick = selectedButtons.Contains(0) ? 300 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 60 : 0;
        }
    }
}
