using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional
{
    public class RadiantArcanum_Tile : ModTile
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
            // name.SetDefault("RadiantArcanum");
            DustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(200, 160, 0), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[]
            {
                TileID.LunarCraftingStation,
                ModContent.TileType<QuantumFusionAccelerator_Tile>()

            };
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.AlchemyTable];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.200f;
            g = 0.160f;
            b = 0f;
        }
    }
}