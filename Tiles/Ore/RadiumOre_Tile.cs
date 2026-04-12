using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class RadiumOre_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;  
            Main.tileLighted[Type] = true;
            Main.tileOreFinderPriority[Type] = 830; 
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<RadiumOre>());
            DustType = ModContent.DustType<Dusts.RadiumDust>();
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Celestial Ore");
            AddMapEntry(new Color(160, 150, 0), name);
			MinPick = 225;
        }
        

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            noItem = true;
            if (Main.dayTime)
                Item.NewItem(Item.GetSource_NaturalSpawn(), new Point(i, j).ToWorldCoordinates(), ModContent.ItemType<RadiumOre>());
            else
                Item.NewItem(Item.GetSource_NaturalSpawn(), new Point(i, j).ToWorldCoordinates(), ModContent.ItemType<DarkmatterOre>());
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Texture2D glowtex;
            if (Main.dayTime)
            {
                glowtex = Mod.GetTexture("Glowmasks/RadiumOre_Glow");
                BaseDrawing.DrawTileTexture(spriteBatch, TextureAssets.Tile[Type].Value, x, y, true, false, false);
                BaseDrawing.DrawTileTexture(spriteBatch, glowtex, x, y, true, false, false, null, AAGlobalTile.GetRadiumColorBright);
            }
            else
            {
                glowtex = Mod.GetTexture("Glowmasks/DarkmatterOre_Glow");
                BaseDrawing.DrawTileTexture(spriteBatch, ModContent.Request<Texture2D>(Texture + "_Darkmatter").Value, x, y, true, false, false);
                BaseDrawing.DrawTileTexture(spriteBatch, glowtex, x, y, true, false, false, null, AAGlobalTile.GetDarkmatterColorBright);
            }
            Tile tile = Main.tile[x, y];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(glowtex, new Vector2((x * 16) - (int)Main.screenPosition.X, (y * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Main.dayTime ? Color.Yellow : Color.DeepSkyBlue, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (Main.dayTime)
            {
                LocalizedText name = CreateMapEntryName();
                AddMapEntry(new Color(160, 150, 0), name);
                DustType = ModContent.DustType<Dusts.RadiumDust>();
            }
            else
            {
                LocalizedText name = CreateMapEntryName();
                AddMapEntry(new Color(0, 30, 100), name);
                DustType = ModContent.DustType<Dusts.DarkmatterDust>();
            }
            return true;
        }


        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = Main.dayTime ? 0.5f : 0f ;
            g = .2f;
            b = Main.dayTime ? 0f : 0.5f;
        }
    }
}