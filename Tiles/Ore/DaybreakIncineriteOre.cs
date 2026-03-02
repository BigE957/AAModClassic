using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class DaybreakIncineriteOre : ModTile
    {

        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 850; 
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            //true for block to emit light
            HitSound = 21;
            Main.tileLighted[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DaybreakIncineriteOre").Type;   
            DustType = Mod.Find<ModDust>("AkumaADust").Type;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Daybreak Incinerite Ore");
            AddMapEntry(new Color(100, 30, 0), name);
			MinPick = 225;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = Mod.GetTexture("Glowmasks/DaybreakIncineriteOre_Glow");
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetAkumaColorBright);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = 0.15f;
            b = 0.15f;
        }
    }
}