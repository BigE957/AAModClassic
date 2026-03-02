using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAMod.UI
{
    internal sealed class TerratoolZUI : TerratoolUI
    {
        public static int Pick = 300;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages => AAMod.instance.GetTexture("UI/Tools/ToolUIZ");

        public override Texture2D ButtonOnImage => AAMod.instance.GetTexture("UI/Tools/ToolButtonZ");

        public override Texture2D ButtonOffImage => AAMod.instance.GetTexture("UI/Tools/ToolButtonZOff");

        public override UIState State => AAMod.instance.TerratoolZState;

        public override int HeldItemType => AAMod.instance.Find<ModItem>("ZeroTerratool").Type;

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);
            Pick = selectedButtons.Contains(0) ? 300 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 60 : 0;
        }
    }
}
