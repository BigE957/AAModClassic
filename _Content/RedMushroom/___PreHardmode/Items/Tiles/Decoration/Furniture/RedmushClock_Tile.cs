using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using Terraria.ModLoader;
using AAModClassic.Dusts;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushClock_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
			TileID.Sets.Clock[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 18];
            TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Height = 5;
			TileObjectData.addTile(Type);

            HitSound = SoundID.Dig;
			DustType = ModContent.DustType<MushDust>();

			AdjTiles = [TileID.GrandfatherClocks];
            VanillaFallbackOnModDeletion = TileID.GrandfatherClocks;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 150, 20), name);

			RegisterItemDrop(ModContent.ItemType<RedmushClock>(), 0);
        }

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override bool RightClick(int x, int y)
        {
            string morningOrEvening = Language.GetTextValue("GameUI.TimeAtMorning");
			double time = Main.time;
			if (!Main.dayTime) {
				time += 54000.0;
			}

			time = (time / 86400.0) * 24.0;
			time = time - 7.5 - 12.0;
			if (time < 0.0) {
				time += 24.0;
			}

			if (time >= 12.0) {
				morningOrEvening = Language.GetTextValue("GameUI.TimePastMorning");
			}

			int hours = (int)time;
			double timeRemainder = time - hours;
			timeRemainder = (int)(timeRemainder * 60.0);
			string minutes = string.Concat(timeRemainder);
			if (timeRemainder < 10.0) {
				minutes = "0" + minutes;
			}

			if (hours > 12) {
				hours -= 12;
			}

			if (hours == 0) {
				hours = 12;
			}

			Main.NewText(Language.GetTextValue("CLI.Time", $"{hours}:{minutes} {morningOrEvening}"), 255, 240, 20);
			return true;
		}
    }
}