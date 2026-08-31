using AAModClassic._Content._Misc.__Hardmode.Items.Consumables;
using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Quest;
using AAModClassic._Content.Stars.World.Altar;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic.Globals
{
    public class AAGlobalTile : GlobalTile
    {
        public static int glowTick = 0;
        public static int glowMax = 100;

        public override void AnimateTile()
        {
            glowTick++;
            if (glowTick >= glowMax)
            {
                glowTick = 0;
            }
        }

        #region Tile Colors

        public static Color GetIncineriteColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(ZAAPlayer.IncineriteColor, color, min, max, clamp);
        public static Color GetIncineriteColorDim(Color color) => GetIncineriteColor(color, 0.4f, 1f, false);
        public static Color GetIncineriteColorBright(Color color) => GetIncineriteColor(color, 0.6f, 1f, false);
        public static Color GetIncineriteColorBrightInvert(Color color) => GetIncineriteColor(color, 1f, 0.6f, true);

        public static Color GetZeroColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.ZeroShield, color, min, max, clamp);
        public static Color GetZeroColorDim(Color color) => GetZeroColor(color, 0.4f, .6f, false);
        public static Color GetZeroColorBright(Color color) => GetZeroColor(color, 0.6f, 1f, false);
        public static Color GetZeroColorBrightInvert(Color color) => GetZeroColor(color, 1f, 0.6f, true);

        public static Color GetTerraColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.LimeGreen, color, min, max, clamp);
        public static Color GetTerraColorDim(Color color) => GetTerraColor(color, 0.4f, 1f, false);
        public static Color GetTerraColorBright(Color color) => GetTerraColor(color, 0.6f, 1f, false);
        public static Color GetTerraColorBrightInvert(Color color) => GetTerraColor(color, 1f, 0.6f, true);

        public static Color GetTerra2Color(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.YellowGreen, color, min, max, clamp);
        public static Color GetTerra2ColorDim(Color color) => GetTerra2Color(color, 0.4f, 1f, false);
        public static Color GetTerra2ColorBright(Color color) => GetTerra2Color(color, 0.6f, 1f, false);
        public static Color GetTerra2ColorBrightInvert(Color color) => GetTerra2Color(color, 1f, 0.6f, true);

        public static Color GetUraniumColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.DarkSeaGreen, color, min, max, clamp);
        public static Color GetUraniumColorDim(Color color) => GetUraniumColor(color, 0.4f, 1f, false);
        public static Color GetUraniumColorBright(Color color) => GetUraniumColor(color, 0.6f, 1f, false);
        public static Color GetUraniumColorBrightInvert(Color color) => GetUraniumColor(color, 1f, 0.6f, true);

        public static Color GetStormColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.Violet, color, min, max, clamp);
        public static Color GetStormColorDim(Color color) => GetStormColor(color, 0.4f, 1f, false);
        public static Color GetStormColorBright(Color color) => GetStormColor(color, 0.6f, 1f, false);
        public static Color GetStormColorBrightInvert(Color color) => GetStormColor(color, 1f, 0.6f, true);

        public static Color GetAkumaColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.DeepSkyBlue, color, min, max, clamp);
        public static Color GetAkumaColorDim(Color color) => GetAkumaColor(color, 0.4f, 1f, false);
        public static Color GetAkumaColorBright(Color color) => GetAkumaColor(color, 0.6f, 1f, false);
        public static Color GetAkumaColorBrightInvert(Color color) => GetAkumaColor(color, 1f, 0.6f, true);

        public static Color GetDarkmatterColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Nightcrawler, color, min, max, clamp);
        public static Color GetDarkmatterColorDim(Color color) => GetDarkmatterColor(color, 0.4f, 1f, false);
        public static Color GetDarkmatterColorBright(Color color) => GetDarkmatterColor(color, 0.6f, 1f, false);
        public static Color GetDarkmatterColorBrightInvert(Color color) => GetDarkmatterColor(color, 1f, 0.6f, true);

        public static Color GetRadiumColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Daybringer, color, min, max, clamp);
        public static Color GetRadiumColorDim(Color color) => GetRadiumColor(color, 0.4f, 1f, false);
        public static Color GetRadiumColorBright(Color color) => GetRadiumColor(color, 0.6f, 1f, false);
        public static Color GetRadiumColorBrightInvert(Color color) => GetRadiumColor(color, 1f, 0.6f, true);

        public static Color GetYamataColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.Maroon, color, min, max, clamp);
        public static Color GetYamataColorDim(Color color) => GetYamataColor(color, 0.4f, 1f, false);
        public static Color GetYamataColorBright(Color color) => GetYamataColor(color, 0.6f, 1f, false);
        public static Color GetYamataColorBrightInvert(Color color) => GetYamataColor(color, 1f, 0.6f, true);

        public static Color GetYamataColor2(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.Violet, color, min, max, clamp);
        public static Color GetYamataColorDim2(Color color) => GetYamataColor2(color, 0.4f, 1f, false);
        public static Color GetYamataColorBright2(Color color) => GetYamataColor2(color, 0.6f, 1f, false);
        public static Color GetYamataColorBrightInvert2(Color color) => GetYamataColor2(color, 1f, 0.6f, true);

        public static Color GetCthulhuColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.DarkCyan, color, min, max, clamp);
        public static Color GetCthulhuColorDim(Color color) => GetCthulhuColor(color, 0.4f, 1f, false);
        public static Color GetCthulhuColorBright(Color color) => GetCthulhuColor(color, 0.6f, 1f, false);
        public static Color GetCthulhuColorBrightInvert(Color color) => GetCthulhuColor(color, 1f, 0.6f, true);

        public static Color GetShenColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Shen2, color, min, max, clamp);
        public static Color GetShenColorDim(Color color) => GetShenColor(color, 0.4f, 1f, false);
        public static Color GetShenColorBright(Color color) => GetShenColor(color, 0.6f, 1f, false);
        public static Color GetShenColorBrightInvert(Color color) => GetShenColor(color, 1f, 0.6f, true);

        public static Color GetSkyColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Sky, color, min, max, clamp);
        public static Color GetSkyColorDim(Color color) => GetSkyColor(color, 0.4f, 1f, false);
        public static Color GetSkyColorBright(Color color) => GetSkyColor(color, 0.6f, 1f, false);
        public static Color GetSkyColorBrightInvert(Color color) => GetSkyColor(color, 1f, 0.6f, true);

        public static Color GetBlankColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.COLOR_WHITEFADE1, color, min, max, clamp);
        public static Color GetBlankColorDim(Color color) => GetBlankColor(color, 0.4f, 1f, false);
        public static Color GetBlankColorBright(Color color) => GetBlankColor(color, 0.6f, 1f, false);
        public static Color GetBlankColorBrightInvert(Color color) => GetBlankColor(color, 1f, 0.6f, true);

        public static Color GetRainbowColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Main.DiscoColor, color, min, max, clamp);
        public static Color GetRainbowColorDim(Color color) => GetRainbowColor(color, 0.4f, 1f, false);
        public static Color GetRainbowColorBright(Color color) => GetRainbowColor(color, 0.6f, 1f, false);
        public static Color GetRainbowColorBrightInvert(Color color) => GetRainbowColor(color, 1f, 0.6f, true);

        #endregion

        public static Color GetTimedrawColor(Color tColor, Color color, float min, float max, bool clamp)
        {
            Color glowColor = BaseUtility.ColorMult(tColor, BaseUtility.MultiLerp(glowTick / (float)glowMax, min, max, min));

            if (clamp)
            {
                if (color.R > glowColor.R) { glowColor.R = color.R; }
                if (color.G > glowColor.G) { glowColor.G = color.G; }
                if (color.B > glowColor.B) { glowColor.B = color.B; }
            }

            return glowColor;
        }

        public static Color GetGradientColor(Color tColor1, Color tColor2, Color color, bool clamp)
        {
            Color glowColor = Color.Lerp(tColor1, tColor2, BaseUtility.MultiLerp(glowTick / (float)glowMax, 0f, 1f, 0f));

            if (clamp)
            {
                if (color.R > glowColor.R)
                {
                    glowColor.R = color.R;
                }

                if (color.G > glowColor.G)
                {
                    glowColor.G = color.G;
                }

                if (color.B > glowColor.B)
                {
                    glowColor.B = color.B;
                }
            }

            return glowColor;
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (TileProtectionSystem.UnbreakableTiles.Contains(new(i, j)))
                return false;

            Tile t = Framing.GetTileSafely(i, j - 1);

            if(!t.HasTile)
                return true;

            if ((t.TileType == ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType == ModContent.TileType<DragonAltarUnsafe_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<AbyssAltarUnsafe_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<DragonAltarUnsafe_Tile>()))
                return false;

            if ((t.TileType == ModContent.TileType<GreedAltar_Tile>() || t.TileType == ModContent.TileType<AcropolisAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<GreedAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<AcropolisAltar_Tile>()))
                return false;

            if ((t.TileType == ModContent.TileType<StarAltar_Tile>() || t.TileType == ModContent.TileType<GravAltar_Tile>() || t.TileType == ModContent.TileType<WormAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<StarAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<GravAltar_Tile>() || Main.tile[i, j].TileType == ModContent.TileType<WormAltar_Tile>()))
                return false;

            return true;
        }

        public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
        {
            if (TileProtectionSystem.UnbreakableTiles.Contains(new(i, j)))
                return false;

            Tile t = Framing.GetTileSafely(i, j - 1);

            if (!t.HasTile)
                return true;

            if ((t.TileType == ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType == ModContent.TileType<DragonAltarUnsafe_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<AbyssAltarUnsafe_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<DragonAltarUnsafe_Tile>()))
                return false;

            if ((t.TileType == ModContent.TileType<GreedAltar_Tile>() || t.TileType == ModContent.TileType<AcropolisAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<GreedAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<AcropolisAltar_Tile>()))
                return false;

            if ((t.TileType == ModContent.TileType<StarAltar_Tile>() || t.TileType == ModContent.TileType<GravAltar_Tile>() || t.TileType == ModContent.TileType<WormAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<StarAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<GravAltar_Tile>() || Main.tile[i, j].TileType == ModContent.TileType<WormAltar_Tile>()))
                return false;

            return true;
        }

        public override bool CanExplode(int i, int j, int type)
        {
            Tile t = Framing.GetTileSafely(i, j - 1);
            if (t.HasTile && (t.TileType == ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType == ModContent.TileType<DragonAltarUnsafe_Tile>()) && (t.TileType != ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType != ModContent.TileType<DragonAltarUnsafe_Tile>()))
                return false;

            if (TileProtectionSystem.UnbreakableTiles.Contains(new(i, j)))
                return false;

            return true;
        }

        public override bool Slope(int i, int j, int type)
        {
            Tile t = Framing.GetTileSafely(i, j - 1);
            if (t.HasTile && (t.TileType == ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType == ModContent.TileType<DragonAltarUnsafe_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<AbyssAltarUnsafe_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<DragonAltarUnsafe_Tile>()))
            {
                return false;
            }

            if (t.HasTile && (t.TileType == ModContent.TileType<GreedAltar_Tile>() || t.TileType == ModContent.TileType<AcropolisAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<GreedAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<AcropolisAltar_Tile>()))
            {
                return false;
            }

            return true;
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            Tile t = Framing.GetTileSafely(i, j - 1);
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                Framing.GetTileSafely(i, j);

            if (t.TileType == TileID.MushroomGrass)
            {
                if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(1000))
                {
                    int style = Main.rand.Next(5);

                    if (PlaceObject(i, j - 1, ModContent.TileType<MadnessMushroom_Tile>(), false, style))
                    {
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MadnessMushroom_Tile>(), style, 0, -1, -1);
                    }
                }
            }

            if (Main.tile[i, j].TileType == TileID.Grass && Main.hardMode)
            {
                if (!Framing.GetTileSafely(i, j - 1).IsTileSolid() && Main.rand.NextBool(800))
                {
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Carrot_Tile>(), false, 0))
                    {
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<Carrot_Tile>(), 0, 0, -1, -1);
                    }
                }
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }

            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
            }

            return false;
        }

        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            ModTile tile = ModContent.GetModTile(type);
            if (tile != null && tile is IGlowmaskTile glowTile && glowTile.ShouldDrawGlow)
            {
                if (ModContent.RequestIfExists(tile.Texture + "_Glow", out Asset<Texture2D> tex))
                {
                    Tile t = Main.tile[i, j];
                    Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
                    if (Main.drawToScreen)
                        zero = Vector2.Zero;

                    Point coordSize = glowTile.GetCoordinateSize(i, j);
                    Main.spriteBatch.Draw(tex.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(t.TileFrameX, t.TileFrameY, coordSize.X, coordSize.Y), glowTile.GlowColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }

    public class AAGlobalWall : GlobalWall
    {
        public override void KillWall(int i, int j, int type, ref bool fail)
        {
            if (TileProtectionSystem.UnbreakableWalls.Contains(new(i, j)))
                fail = true;
        }

        public override bool CanExplode(int i, int j, int type)
        {
            if (TileProtectionSystem.UnbreakableWalls.Contains(new(i, j)))
                return false;
            return base.CanExplode(i, j, type);
        }
    }

    public class TileProtectionSystem : ModSystem
    {
        public static readonly HashSet<Point> UnbreakableTiles = [];

        public static readonly HashSet<Point> UnbreakableWalls = [];

        public override void SaveWorldData(TagCompound tag)
        {
            tag["ProtectedTileList"] = UnbreakableTiles.ToList();
            tag["ProtectedWallList"] = UnbreakableWalls.ToList();
        }

        public override void ClearWorld()
        {
            UnbreakableTiles.Clear();
            UnbreakableWalls.Clear();
        }

        public override void LoadWorldData(TagCompound tag)
        {
            UnbreakableTiles.Clear();
            var list = tag.GetList<Point>("ProtectedTileList");
            foreach(Point p in list)
                UnbreakableTiles.Add(p);

            UnbreakableWalls.Clear();
            list = tag.GetList<Point>("ProtectedWallList");
            foreach (Point p in list)
                UnbreakableWalls.Add(p);
        }

        public static void UnprotectTiles(params int[] types) => UnbreakableTiles.RemoveWhere((p) => types.Contains(Main.tile[p].TileType));

        public static void UnprotectWalls(params int[] types) => UnbreakableWalls.RemoveWhere((p) => types.Contains(Main.tile[p].WallType));
    }

    public interface IGlowmaskTile
    {
        public bool ShouldDrawGlow => true;
        public Point GetCoordinateSize(int x, int y) => new(16, 16);
        public Color GlowColor => Color.White;
    }
}