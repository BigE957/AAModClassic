using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unofficial.Content.SunkenShip.___PreHardmode.Items
{
    public class SunkenShipBox_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            TileID.Sets.DisableSmartCursor[Type] = true;
            DustType = ModContent.DustType<CthulhuDust>();
            AddMapEntry(new Color(200, 200, 200), Language.GetText("ItemName." + ItemID.Search.GetName(ItemID.MusicBox)));
            RegisterItemDrop(ModContent.ItemType<SunkenShipBox>());
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<SunkenShipBox>();
        }
    }
}
