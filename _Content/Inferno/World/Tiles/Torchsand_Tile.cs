using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class Torchsand_Tile : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSand[Type] = true;
            RegisterItemDrop(ModContent.ItemType<Torchsand>());
            AddMapEntry(new Color(50, 35, 22));
            TileID.Sets.Conversion.Sand[Type] = true;
            DustType = ModContent.DustType<Dusts.RazewoodDust>();
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tile = Main.tile[i, j];
            Tile tile2 = Main.tile[i, j - 1];
            Tile tile3 = Main.tile[i, j + 1];
            int tileType = tile.TileType;
            if (!WorldGen.noTileActions && tile.HasTile && (tileType == Type))
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (tile3 != null && !tile3.HasTile)
                    {
                        bool flag18 = !(tile2.HasTile && (TileID.Sets.BasicChest[tile2.TileType] || TileID.Sets.BasicChestFake[tile2.TileType] || tile2.TileType == TileID.PalmTree || TileID.Sets.BasicDresser[tile2.TileType]));
                        if (flag18)
                        {
                            int damage = 10;
                            int projectileType = 0;
                            if (tileType == Type)
                            {
                                projectileType = ModContent.ProjectileType<TorchsandBall>();
                                damage = 0;
                            }
                            tile.ClearTile();
                            int num77 = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), i * 16 + 8, j * 16 + 8, 0f, 0.41f, projectileType, damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num77].ai[0] = 1f;
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                    }
                }
                else if (Main.netMode == NetmodeID.Server && tile3 != null && !tile3.HasTile)
                {
                    bool flag19 = !(tile2.HasTile && (TileID.Sets.BasicChest[tile2.TileType] || TileID.Sets.BasicChestFake[tile2.TileType] || tile2.TileType == TileID.PalmTree || TileID.Sets.BasicDresser[tile2.TileType]));
                    if (flag19)
                    {
                        int damage2 = 10;
                        int projectileType = 0;
                        if (tileType == Type)
                        {
                            projectileType = ModContent.ProjectileType<TorchsandBall>();
                            damage2 = 0;
                        }

                        tile.HasTile = false;
                        bool flag20 = false;
                        foreach (Projectile p in Main.ActiveProjectiles)
                        {
                            if (p.owner == Main.myPlayer && p.type == projectileType && Math.Abs(p.timeLeft - 3600) < 60 && p.Distance(new Vector2(i * 16 + 8, j * 16 + 10)) < 4f)
                            {
                                flag20 = true;
                                break;
                            }
                        }
                        if (!flag20)
                        {
                            int num79 = Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), i * 16 + 8, j * 16 + 8, 0f, 2.5f, projectileType, damage2, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num79].velocity.Y = 0.5f;
                            Projectile expr_7AAA_cp_0 = Main.projectile[num79];
                            expr_7AAA_cp_0.position.Y += 2f;
                            Main.projectile[num79].netUpdate = true;
                        }
                        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                        WorldGen.SquareTileFrame(i, j, true);
                    }
                }
            }
            return true;
        }
    }
}