using AAModClassic._Content._EX._PostMoonlord.Items.Tools;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI
{
    internal sealed class TerratoolEXUI : TerratoolUI
    {
        public static int Pick = 320;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Asset<Texture2D> ButtonImages => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolUI");

        public override Asset<Texture2D> ButtonOnImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButton");

        public override Asset<Texture2D> ButtonOffImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButtonOff");

        public override UIState State => AAMod.instance.TerratoolEXState;

        public override int HeldItemType => ModContent.ItemType<Terraformer>();

        public override void ButtonClicked(UIMouseEvent evt, UIElement element)
        {
            base.ButtonClicked(evt, element);
            Pick = selectedButtons.Contains(0) ? 320 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 70 : 0;
        }
    }
}
