using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus
{
    public abstract class ChandelierTile : ModTile
    {
        public abstract int ItemType { get; }

        public abstract Color LightColor { get; }

        public abstract int HitDust { get; }

        public virtual int FlameDust => -1;

        private static Dictionary<int, Asset<Texture2D>> flameTextures = [];
        private static Dictionary<int, Asset<Texture2D>> glowTextures = [];

        public override void SetStaticDefaults()
        {
            if (ModContent.RequestIfExists<Texture2D>(Texture + "_Flame", out var flameAsset))
                flameTextures.Add(Type, flameAsset);

            if (ModContent.RequestIfExists<Texture2D>(Texture + "_Glow", out var glowAsset))
                glowTextures.Add(Type, glowAsset);

            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileLighted[Type] = true;
            // We don't set Main.tileFlame

            TileID.Sets.MultiTileSway[Type] = true;
            TileID.Sets.IsAMechanism[Type] = true;

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Point16(1, 0);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.DrawYOffset = -2;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(235, 166, 135), Language.GetText("MapObject.Chandelier"));

            DustType = HitDust;

            // Since we are using RandomStyleRange without StyleMultiplier, we'll need to manually register the item drop for the tile styles other than style 0. Here we register the default drop for any style.
            RegisterItemDrop(ItemType);
        }

        public override void HitWire(int i, int j)
        {
            FurnitureCommon.LightHitWire(Type, i, j, 3, 3);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (Main.tile[i, j].TileFrameX >= 18)
                return;

            Vector3 color = LightColor.ToVector3();
            r = color.X;
            g = color.Y;
            b = color.Z;
        }

        public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
        {
            if (Main.rand.NextBool(40) && tileFrameY < 54)
            {
                int tileColumn = tileFrameX / 18 % 3;
                if (tileFrameY / 18 % 3 == 1 && tileColumn != 1)
                {
                    if (FlameDust != -1)
                    {
                        Dust dust = Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 2), 14, 6, FlameDust, 0f, 0f, 100);
                        if (Main.rand.NextBool(3))
                            dust.noGravity = true;

                        dust.velocity *= 0.3f;
                        dust.velocity.Y -= 1.5f;
                    }
                }
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];

            if (TileObjectData.IsTopLeft(tile))
            {
                // Makes this tile sway in the wind and with player interaction when used with TileID.Sets.MultiTileSway
                Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
            }

            // We must return false here to prevent the normal tile drawing code from drawing the default static tile. Without this a duplicate tile will be drawn.
            return false;
        }

        public override void AdjustMultiTileVineParameters(int i, int j, ref float? overrideWindCycle, ref float windPushPowerX, ref float windPushPowerY, ref bool dontRotateTopTiles, ref float totalWindMultiplier, ref Texture2D glowTexture, ref Color glowColor)
        {
            // Vanilla chandeliers all share these parameters.
            overrideWindCycle = 1f;
            windPushPowerY = 0;

            if(glowTextures.TryGetValue(Type, out var glow))
            {
                glowTexture = glow.Value;
                glowColor = Color.White;
            }

            /*
            switch (style)
            {
                case StyleID.Silver:
                    // The silver style is a typical chandelier with no additional customizations.
                    break;
                case StyleID.Frozen:
                    // The frozen style is stiffer and moves half as much as the default.
                    totalWindMultiplier *= 0.5f;
                    break;
                case StyleID.PalmWood:
                    // The palm wood style is completely rigid and does not move at all.
                    overrideWindCycle = 0f;
                    break;
                case StyleID.BorealWood:
                    // The boreal wood style
                    overrideWindCycle = null;
                    windPushPowerY = -1f;
                    dontRotateTopTiles = true;
                    // Additional glowmask
                    glowTexture = this.glowTexture.Value;
                    glowColor = Color.White;
                    break;
                case StyleID.Flesh:
                    overrideWindCycle = null;
                    windPushPowerY = -1f;
                    dontRotateTopTiles = true;
                    totalWindMultiplier *= 0.3f;
                    break;
            }
            */
        }

        public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
        {
            if (!flameTextures.TryGetValue(Type, out var flame))
                return;

            ulong flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

            tileFlameData.flameTexture = flame.Value;
            tileFlameData.flameSeed = flameSeed;

            tileFlameData.flameCount = 7;
            tileFlameData.flameColor = new Color(100, 100, 100, 0);
            tileFlameData.flameRangeXMin = -10;
            tileFlameData.flameRangeXMax = 11;
            tileFlameData.flameRangeYMin = -10;
            tileFlameData.flameRangeYMax = 1;
            tileFlameData.flameRangeMultX = 0.15f;
            tileFlameData.flameRangeMultY = 0.35f;
            /*
            StyleID style = (StyleID)TileObjectData.GetTileStyle(Main.tile[i, j]);

            switch (style)
            {
                case StyleID.Flesh:
                    tileFlameData.flameCount = 3;
                    tileFlameData.flameColor = new Color(50, 50, 50, 0);
                    tileFlameData.flameRangeXMin = -10;
                    tileFlameData.flameRangeXMax = 11;
                    tileFlameData.flameRangeYMin = -10;
                    tileFlameData.flameRangeYMax = 11;
                    tileFlameData.flameRangeMultX = 0.05f;
                    tileFlameData.flameRangeMultY = 0.15f;
                    break;
                case StyleID.Frozen:
                    tileFlameData.flameCount = 7;
                    tileFlameData.flameColor = new Color(50, 50, 50, 0);
                    tileFlameData.flameRangeXMin = -10;
                    tileFlameData.flameRangeXMax = 11;
                    tileFlameData.flameRangeYMin = -10;
                    tileFlameData.flameRangeYMax = 11;
                    tileFlameData.flameRangeMultX = 0.3f;
                    tileFlameData.flameRangeMultY = 0.3f;
                    break;
                default:
                    tileFlameData.flameCount = 7;
                    tileFlameData.flameColor = new Color(100, 100, 100, 0);
                    tileFlameData.flameRangeXMin = -10;
                    tileFlameData.flameRangeXMax = 11;
                    tileFlameData.flameRangeYMin = -10;
                    tileFlameData.flameRangeYMax = 1;
                    tileFlameData.flameRangeMultX = 0.15f;
                    tileFlameData.flameRangeMultY = 0.35f;
                    break;
            }
            */
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            /*
            Tile tile = Main.tile[i, j];
            int topX = i - tile.TileFrameX % 54 / 18;
            int topY = j - tile.TileFrameY % 54 / 18;
            if (tile.TileFrameY / 54 == 0 && Animation.GetTemporaryFrame(topX, topY, out int frameData))
                frameYOffset = 54 * frameData;
            */
        }
    }
}
