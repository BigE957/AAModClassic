using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Decoration
{
    public class RoyalBunnyCage : ModTile
    {
        public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(Type);
            disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;

			AnimationFrameHeight = 54;

			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Royal Bunny Cage");
			AddMapEntry(Color.Gold, name);
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, Mod.Find<ModItem>("RoyalBunnyCage").Type);
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (frame == 0)
            {
                frameCounter ++;
                if (frameCounter > Main.rand.Next(30, 900))
                {
                    if (Main.rand.Next(3) != 0)
                    {
                        int k = Main.rand.Next(7);
                        if (k == 0)
                        {
                            frame = 4;
                        }
                        else if (k <= 2)
                        {
                            frame = 2;
                        }
                        else
                        {
                            frame = 1;
                        }
                    }
                    frameCounter = 0;
                }
            }
            else if (frame == 1)
            {
                frameCounter ++;
                if (frameCounter >= 10)
                {
                    frameCounter = 0;
                    frame = 0;
                }
            }
            else if (frame >= 2 && frame <= 3)
            {
                frameCounter ++;
                if (frameCounter >= 10)
                {
                    frameCounter = 0;
                    frame ++;
                }
                if (frame > 3)
                {
                    frame = 0;
                }
            }
            else if (frame >= 4 && frame <= 10)
            {
                frameCounter ++;
                if (frameCounter >= 5)
                {
                    frameCounter = 0;
                    frame ++;
                }
            }
            else if (frame == 11)
            {
                frameCounter ++;
                if (frameCounter > Main.rand.Next(30, 900))
                {
                    if (Main.rand.Next(3) != 0)
                    {
                        if (Main.rand.Next(7) == 0)
                        {
                            frame = 17;
                        }
                        else if(Main.rand.Next(12) == 0)
                        {
                            frame = 13;
                        }
                        else
                        {
                            frame = 12;
                        }
                    }
                    frameCounter = 0;
                }
            }
            else if (frame == 12)
            {
                frameCounter ++;
                if (frameCounter >= 10)
                {
                    frameCounter = 0;
                    frame = 11;
                }
            }
            else if (frame >= 13 && frame <= 14)
            {
                frameCounter ++;
                if (frameCounter >= 30)
                {
                    frameCounter = 0;
                    frame ++;
                }
            }
            else if (frame == 15)
            {
                frameCounter ++;
                if (frameCounter > 20)
                {
                    if (Main.rand.Next(3) != 0)
                    {
                        if (Main.rand.Next(3) == 0)
                        {
                            frame = 16;
                        }
                        else
                        {
                            frame = 13;
                        }
                    }
                    frameCounter = 0;
                }
            }
            else if (frame == 16)
            {
                frameCounter ++;
                if (frameCounter >= Main.rand.Next(30, 900))
                {
                    frameCounter = 0;
                    frame ++;
                }
            }
            else if (frame >= 17)
            {
                frameCounter ++;
                if (frameCounter >= 5)
                {
                    frameCounter = 0;
                    frame ++;
                }
                if (frame > 25)
                {
                    frame = 0;
                }
            }
        }
    }
}