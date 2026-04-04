using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Crafters
{
    public class TerraCore : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Core of Terraria");
            DustType = DustID.Terra;
            AddMapEntry(new Color(40, 200, 0), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[]
            {
                TileID.LunarCraftingStation,
                TileID.WorkBenches,
                TileID.Hellforge,
                TileID.Furnaces,
                TileID.TinkerersWorkbench,
                TileID.AlchemyTable,
                TileID.Bottles,
                TileID.MythrilAnvil,
                TileID.Tables,
                TileID.DemonAltar,
                TileID.Chairs,
                TileID.Anvils,
                ModContent.TileType<HellstoneAnvil>(),
                ModContent.TileType<HallowedAnvil>(),
                ModContent.TileType<HallowedForge>(),
                ModContent.TileType<QuantumFusionAccelerator>(),
                TileID.MythrilAnvil,
                TileID.Anvils,
                TileID.CrystalBall,
                TileID.HeavyWorkBench,
                TileID.Hellforge,
                TileID.Furnaces,
                TileID.AdamantiteForge,
                TileID.Autohammer,
                TileID.ImbuingStation
            };
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.AlchemyTable];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.50f;
            g = 0;
            b = 0f;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Texture2D texture = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady(Type, (int)tile.Slope, tile.TileColor);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            int animate = 0;
            if (tile.TileFrameY >= 56)
            {
                animate = Main.tileFrame[Type] * AnimationFrameHeight;
            }
            Main.spriteBatch.Draw(texture, new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY + animate, 16, height), Lighting.GetColor(i, j), 0f, default, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Mod.GetTexture("Glowmasks/TerraCore_Glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY + animate, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}