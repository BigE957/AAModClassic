using AAModClassic._Content.Chaos.__Hardmode.Items.Tools;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI
{
    internal sealed class TerratoolCUI : TerratoolUI
    {
        public static int Pick = 215;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Asset<Texture2D> ButtonImages => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolUIC");

        public override Asset<Texture2D> ButtonOnImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButtonC");

        public override Asset<Texture2D> ButtonOffImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButtonCOff");

        public override UIState State => AAMod.instance.TerratoolCState;

        public override int HeldItemType => ModContent.ItemType<ChaosTerratool>();

        public override void ButtonClicked(UIMouseEvent evt, UIElement element)
        {
            base.ButtonClicked(evt, element);
            Pick = selectedButtons.Contains(0) ? 215 : 0;
            Hammer = selectedButtons.Contains(1) ? 120 : 0;
            Axe = selectedButtons.Contains(2) ? 50 : 0;
        }
    }
}
