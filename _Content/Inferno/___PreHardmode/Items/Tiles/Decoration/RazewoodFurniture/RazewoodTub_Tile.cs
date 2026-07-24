using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodTub_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpBathtub(ModContent.ItemType<RazewoodTub>());
            DustType = ModContent.DustType<RazewoodDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}