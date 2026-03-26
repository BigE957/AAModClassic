using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Razewood
{
    public class RazewoodClock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new[]
            {
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Example Clock"); // Automatic from .lang files
            AddMapEntry(new Color(205, 62, 12), name);
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
            AdjTiles = new int[] { TileID.GrandfatherClocks };
        }

        public override bool RightClick(int x, int y)
        {
            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.RazewoodClockGetTime"), 205, 62, 12);
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.SceneMetrics.HasClock = true;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}