using Microsoft.Xna.Framework;
using SpiritReforged.Content.Forest.Walls;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.SpiritReforged.Trellises
{
    [JITWhenModsEnabled("SpiritReforged")]
    public class TrellisCheckItemUse : GlobalItem
    {
        public override bool? UseItem(Item item, Player player)
        {
            if (player.whoAmI != Main.myPlayer)
                return null;

            Point target = new(Player.tileTargetX, Player.tileTargetY);

            if (!player.InInteractionRange(target.X, target.Y, TileReachCheckSettings.Simple))
                return null;

            Tile tile = Framing.GetTileSafely(target);

            if (tile.HasTile || tile.WallType == WallID.None)
                return null;

            if (WallLoader.GetWall(tile.WallType) is Trellis trellis && trellis.Mod == Mod)
                return trellis.CheckItemUse(item.type, player, target.X, target.Y);

            return null;
        }
    }
}
