using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Crafters
{
    public class SharpeningLavaFishTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.newTile.AnchorInvalidTiles = new[] { 127 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Sharpening Lava Fish");
            DustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(223, 113, 38), name);
            TileID.Sets.DisableSmartCursor[Type] = false;
            AdjTiles = new int[]
            {
                ModContent.TileType<SharpeningLavaFishTile>()
            };
            AnimationFrameHeight = 38;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
			if (frameCounter >= 10)
			{
				frameCounter = 0;
				frame++;
				if (frame >= 4)
				{
					frame = 0;
				}
			}
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
            offsetY = 2;
		}

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = .99f;
            g = .44f;
            b = .15f;
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.AddBuff(BuffID.Sharpened, 36000, true);
            player.AddBuff(BuffID.WeaponImbueFire, 36000, true);
			SoundEngine.PlaySound(SoundID.Item37, player.position);
            return true;
        }

        public override void MouseOver(int i, int j)
        {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<SharpeningLavaFish>();
		}
    }
}