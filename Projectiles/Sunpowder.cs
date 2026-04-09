using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.___Content.Mire.World.Tiles;

namespace AAModClassic.Projectiles
{
    internal class Sunpowder : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            int dustType = ModContent.DustType<Dusts.BroodmotherDust>();
            Projectile.velocity *= 0.95f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] == 180f)
            {
                Projectile.Kill();
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                for (int num62 = 0; num62 < 30; num62++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X, Projectile.velocity.Y, 50);
                }
            }
            int num63 = (int)(Projectile.position.X / 16f) - 1;
            int num64 = (int)((Projectile.position.X + Projectile.width) / 16f) + 2;
            int num65 = (int)(Projectile.position.Y / 16f) - 1;
            int num66 = (int)((Projectile.position.Y + Projectile.height) / 16f) + 2;
            if (num63 < 0)
            {
                num63 = 0;
            }
            if (num64 > Main.maxTilesX)
            {
                num64 = Main.maxTilesX;
            }
            if (num65 < 0)
            {
                num65 = 0;
            }
            if (num66 > Main.maxTilesY)
            {
                num66 = Main.maxTilesY;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                Convert((int)(Projectile.position.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16);
            }
        }

        public static void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = Main.tile[k, l].TileType;
                        int wall = Main.tile[k, l].WallType;
                        if (type == (ushort)ModContent.WallType<DepthstoneWall_Wall>())
                        {
                            Main.tile[k, l].WallType = WallID.Stone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == (ushort)ModContent.WallType<DepthsandstoneWall_Wall>())
                        {
                            Main.tile[k, l].WallType = WallID.Sandstone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == (ushort)ModContent.WallType<DepthsandHardenedWall_Wall>())
                        {
                            Main.tile[k, l].WallType = WallID.HardenedSand;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Depthstone_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Stone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<MireGrass_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.JungleGrass;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<IndigoIce_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.IceBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Depthsandstone_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Sandstone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Depthsand_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.Sand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<DepthsandHardened_Tile>())
                        {
                            Main.tile[k, l].TileType = TileID.HardenedSand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                    }
                }
            }
        }
    }
}
