using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Tiles;

namespace AAModClassic.___Content.Void._PostMoonlord.Items.Materials
{
    public class ApocalyptiteOre_Tile : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<Doomstone_Tile>()] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileOreFinderPriority[Type] = 860;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<ApocalyptiteOre>());
            DustType = ModContent.DustType<Dusts.DoomDust>();
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Apocalyptite Ore");
            AddMapEntry(new Color(70, 20, 20), name);
			MinPick = 225;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseUtility.ColorMult(AAPlayer.ZeroColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/ApocalyptiteTile_Glow").Value;
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetZeroColorDim);
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}