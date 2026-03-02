using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAMod.UI
{
    internal sealed class TerratoolTUI : TerratoolUI
    {
        public static int Pick = 215;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages => AAMod.instance.GetTexture("UI/Tools/ToolUI");

        public override Texture2D ButtonOnImage => AAMod.instance.GetTexture("UI/Tools/ToolButton");

        public override Texture2D ButtonOffImage => AAMod.instance.GetTexture("UI/Tools/ToolButtonOff");

        public override UIState State => AAMod.instance.TerratoolTState;

        public override int HeldItemType => AAMod.instance.Find<ModItem>("Terratool").Type;

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);
            Pick = selectedButtons.Contains(0) ? 215 : 0;
            Hammer = selectedButtons.Contains(1) ? 120 : 0;
            Axe = selectedButtons.Contains(2) ? 50 : 0;
        }
    }
}
