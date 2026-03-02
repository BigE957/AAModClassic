using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class IncineriteOre : ModTile
    {
        public Texture2D glowTex;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 340; 
            Main.tileMerge[Type][Mod.Find<ModTile>("Torchstone").Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("Incinerite").Type;   
            DustType = Mod.Find<ModDust>("IncineriteDust").Type;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Incinerite Ore");
            AddMapEntry(new Color(204, 102, 0), name);
			MinPick = 65;
        }


        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(AAPlayer.IncineriteColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = Mod.GetTexture("Glowmasks/IncineriteOre_glow");
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetIncineriteColorDim);
            }
        }
    }
}