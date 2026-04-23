using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using AAModClassic._Content.Mire.__Hardmode.Items.Consumables;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Tiles
{
    public class ToxinMonkfish_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
            TileObjectData.newTile.AnchorInvalidTiles = new[] { 127 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Toxin Monkfish");
            DustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(93, 163, 79), name);
            TileID.Sets.DisableSmartCursor[Type] = false;
            AdjTiles = new int[]
            {
                TileID.AlchemyTable,
                ModContent.TileType<ToxinMonkfish_Tile>()
            };
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
			if (frameCounter >= 4)
			{
				frameCounter = 0;
				frame++;
				if (frame >= 4)
				{
					frame = 0;
				}
			}
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.36f;
            g = 0.64f;
            b = 0.31f;
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.AddBuff(ModContent.BuffType<FlaskOfHydratoxin_Buff>(), 36000, true);
			SoundEngine.PlaySound(SoundID.Grab, player.position);
            return true;
        }

        public override void MouseOver(int i, int j)
        {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<ToxinMonkfish>();
		}
    }
}