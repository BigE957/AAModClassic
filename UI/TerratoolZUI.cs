using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI
{
    internal sealed class TerratoolZUI : TerratoolUI
    {
        public static int Pick = 300;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Asset<Texture2D> ButtonImages => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolUIZ");

        public override Asset<Texture2D> ButtonOnImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButtonZ");

        public override Asset<Texture2D> ButtonOffImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButtonZOff");

        public override UIState State => AAMod.instance.TerratoolZState;

        public override int HeldItemType => AAMod.instance.Find<ModItem>("ZeroTerratool").Type;

        public override void ButtonClicked(UIMouseEvent evt, UIElement element)
        {
            base.ButtonClicked(evt, element);
            Pick = selectedButtons.Contains(0) ? 300 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 60 : 0;
        }
    }
}
