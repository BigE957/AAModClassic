using AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration.BogwoodFurniture
{
    public class BogwoodBathtub_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpBathtub(ModContent.ItemType<DoomBathtub>());
            DustType = ModContent.DustType<BogwoodDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}