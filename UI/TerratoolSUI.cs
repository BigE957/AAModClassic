using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI
{
    internal sealed class TerratoolSUI : TerratoolUI
    {
        public static int Pick = 320;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages => AAMod.instance.GetTexture("UI/Tools/ToolUIS");

        public override Texture2D ButtonOnImage => AAMod.instance.GetTexture("UI/Tools/ToolButtonS");

        public override Texture2D ButtonOffImage => AAMod.instance.GetTexture("UI/Tools/ToolButtonSOff");

        public override UIState State => AAMod.instance.TerratoolSState;

        public override int HeldItemType => AAMod.instance.Find<ModItem>("ShenTerratool").Type;

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);
            Pick = selectedButtons.Contains(0) ? 320 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 70 : 0;
        }
    }
}
