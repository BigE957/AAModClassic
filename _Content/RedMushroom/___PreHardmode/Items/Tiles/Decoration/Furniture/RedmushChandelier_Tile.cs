using AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushChandelier_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpChandelier(ModContent.ItemType<RedmushChandelier>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override void HitWire(int i, int j) => FurnitureCommon.LightHitWire(Type, i, j, 3, 3);

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX == 0) {
				r = 1.1f;
				g = 0.8f;
				b = 0.8f;
			}
		}

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => DrawingUtils.DrawSwayingMultiTile(i, j);
    }
}