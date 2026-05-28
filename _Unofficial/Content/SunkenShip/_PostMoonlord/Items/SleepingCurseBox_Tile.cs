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

namespace AAModClassic._Unofficial.Content.SunkenShip._PostMoonlord.Items
{
    public class SleepingCurseBox_Tile : ModTile
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
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Music Box");
            DustType = ModContent.DustType<CthulhuDust>();
            AddMapEntry(new Color(200, 200, 200), name);
            RegisterItemDrop(ModContent.ItemType<SleepingCurseBox>());
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<SleepingCurseBox>();
        }
    }
}
