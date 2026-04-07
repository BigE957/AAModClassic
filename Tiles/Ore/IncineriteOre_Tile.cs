using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class IncineriteOre_Tile : ModTile
    {
        public Texture2D glowTex;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 340; 
            Main.tileMerge[Type][ModContent.TileType<Torchstone_Tile>()] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<IncineriteOre>());   
            DustType = ModContent.DustType<Dusts.IncineriteDust>();
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