using AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushSink_Tile : ModTile 
    {
        public override void SetStaticDefaults()
        {
            this.SetUpSink(ModContent.ItemType<RedmushSink>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}