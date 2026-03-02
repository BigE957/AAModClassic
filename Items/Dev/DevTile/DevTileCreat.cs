using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.Dev.DevTile
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
                        bool canplace = (type == Mod.Find<ModTile>("MireGrass").Type || type == Mod.Find<ModTile>("Depthstone").Type) && (Main.tile[i + 1, j - 1].TileType == Mod.Find<ModTile>("MireGrass").Type || type == Mod.Find<ModTile>("Depthstone").Type) && !Main.tile[i, j - 1].HasTile && !Main.tile[i + 1, j - 1].HasTile && j > Main.worldSurface + 200;
                        if(canplace)
                        {
                            WorldGen.PlaceTile(i, j - 1, Mod.Find<ModTile>("CCMireBox").Type, true, false);
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
                            WorldGen.PlaceTile(i, j - 1, Mod.Find<ModTile>("InvokerBookTile").Type, true, false);
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