using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Tiles;
using AAModClassic.Walls;
using AAModClassic.Dusts;

namespace AAModClassic.Projectiles
{
    internal class Moonpowder : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
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
            int dustType = ModContent.DustType<AbyssDust>();
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

        public void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = Main.tile[k, l].TileType;
                        int wall = Main.tile[k, l].WallType;
                        if (type == (ushort)ModContent.WallType<TorchstoneWall>())
                        {
                            Main.tile[k, l].WallType = WallID.Stone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == (ushort)ModContent.WallType<TorchsandstoneWall>())
                        {
                            Main.tile[k, l].WallType = WallID.Sandstone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == (ushort)ModContent.WallType<TorchsandHardenedWall>())
                        {
                            Main.tile[k, l].WallType = WallID.HardenedSand;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Torchstone>())
                        {
                            Main.tile[k, l].TileType = TileID.Stone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<InfernoGrass>())
                        {
                            Main.tile[k, l].TileType = TileID.Grass;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Torchice>())
                        {
                            Main.tile[k, l].TileType = TileID.IceBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Torchsandstone>())
                        {
                            Main.tile[k, l].TileType = TileID.Sandstone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Torchsand>())
                        {
                            Main.tile[k, l].TileType = TileID.Sand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<TorchsandHardened>())
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
