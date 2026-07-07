using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity;
using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;

namespace AAModClassic._Content._Dev.World
{
    public class DevTileCreat : GlobalTile
    {
        public override void RandomUpdate(int i, int j, int type)
		{
            if (Main.expertMode)
            {
                if(DevWorld.CCBoxSetOK)
                {
                    if(AAWorld.downedEquinox)
                    {
                        bool canplace = (type == ModContent.TileType<MireGrass_Tile>() || type == ModContent.TileType<Depthstone_Tile>()) && (Main.tile[i + 1, j - 1].TileType == ModContent.TileType<MireGrass_Tile>() || type == ModContent.TileType<Depthstone_Tile>()) && !Main.tile[i, j - 1].HasTile && !Main.tile[i + 1, j - 1].HasTile && j > Main.worldSurface + 200;
                        if(canplace)
                        {
                            WorldGen.PlaceTile(i, j - 1, ModContent.TileType<CCBag_Tile>(), true, false);
                            DevWorld.CCBoxSetOK = false;
                            if (Main.netMode == NetmodeID.Server && Main.tile[i, j].HasTile)
                            {
                                NetMessage.SendTileSquare(-1, i, j, 1, 0);
                            }
                        }
                    }
                }
                if(DevWorld.InvokerBookSetOK)
                {
                    if(NPC.downedPlantBoss)
                    {
                        bool canplace = type == 19 && (Main.tile[i, j].TileFrameY == 10 * 18 || Main.tile[i, j].TileFrameY == 11 * 18) && !Main.tile[i, j - 1].HasTile;
                        if(canplace)
                        {
                            WorldGen.PlaceTile(i, j - 1, ModContent.TileType<AleisterBook_Tile>(), true, false);
                            DevWorld.InvokerBookSetOK = false;
                            if (Main.netMode == NetmodeID.Server && Main.tile[i, j].HasTile)
                            {
                                NetMessage.SendTileSquare(-1, i, j, 1, 0);
                            }
                        }
                    }
                }
            }
		}
    }
}