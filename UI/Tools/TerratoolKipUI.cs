using AAModClassic._Content._Dev._PostMoonlord.Items.Tools;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI.Tools
{
    public sealed class TerratoolKipUI : TerratoolUI
    {
        public static int Pick = 320;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Asset<Texture2D> ButtonImages => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolUIKip");

        public override Asset<Texture2D> ButtonOnImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButtonKip");

        public override Asset<Texture2D> ButtonOffImage => ModContent.Request<Texture2D>("AAModClassic/UI/Tools/ToolButtonKipOff");

        public override UIState State => AAMod.instance.TerratoolKipState;

        public override int HeldItemType => ModContent.ItemType<ExtravagantTerratool>();

        public override void ButtonClicked(UIMouseEvent evt, UIElement element)
        {
            base.ButtonClicked(evt, element);
            Pick = selectedButtons.Contains(0) ? 320 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 70 : 0;
        }
    }
}
