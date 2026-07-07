using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.BossStandard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.BossStandard
{
    public class RetrieverTrophy_Tile : TrophyTileAbstract, IGlowmaskTile
	{
        public Color GlowColor => AAColor.Flash;
	}
}