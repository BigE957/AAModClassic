using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration.Ancient;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
    public class DataBank_Tile : AncientDataBank_Tile, IGlowmaskTile
    {
        public Color GlowColor => BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, Color.White, Color.White, Color.Violet, Color.White, Color.Violet, Color.White, Color.White, Color.White, Color.White, Color.Violet, Color.White, Color.Violet);
    }
}