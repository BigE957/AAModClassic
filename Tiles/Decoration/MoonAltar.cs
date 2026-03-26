using AAModClassic;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Decoration
{
    public class MoonAltar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(75, 139, 166));
            DustType = DustID.Stone;
            AnimationFrameHeight = 56;
            TileID.Sets.DisableSmartCursor[Type] = true;
            DustType = ModContent.DustType<YamataDust>();
            AdjTiles = new int[] { TileID.LunarMonolith };
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.tile[i, j].TileFrameY >= 56)
            {
                AAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AAPlayer>();
                modPlayer.MoonAltar = true;
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.LunarMonolith];
            frameCounter = Main.tileFrameCounter[TileID.LunarMonolith];
        }

        public override bool RightClick(int i, int j)
        {
            SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16, j * 16));
            HitWire(i, j);
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = Mod.Find<ModItem>("MoonAltar").Type;
        }

        public override void HitWire(int i, int j)
        {
            int x = i - (Main.tile[i, j].TileFrameX / 18 % 2);
            int y = j - (Main.tile[i, j].TileFrameY / 18 % 3);
            for (int l = x; l < x + 2; l++)
            {
                for (int m = y; m < y + 3; m++)
                {
                    if (Main.tile[l, m] == null)
                        continue;
                    if (Main.tile[l, m].HasTile && Main.tile[l, m].TileType == Type)
                    {
                        if (Main.tile[l, m].TileFrameY < 56)
                        {
                            Main.tile[l, m].TileFrameY += 56;
                        }
                        else
                        {
                            Main.tile[l, m].TileFrameY -= 56;
                        }
                    }
                }
            }
            if (Wiring.running)
            {
                Wiring.SkipWire(x, y);
                Wiring.SkipWire(x, y + 1);
                Wiring.SkipWire(x, y + 2);
                Wiring.SkipWire(x + 1, y);
                Wiring.SkipWire(x + 1, y + 1);
                Wiring.SkipWire(x + 1, y + 2);
            }
            NetMessage.SendTileSquare(-1, x, y + 1, 3);
        }
    }
}