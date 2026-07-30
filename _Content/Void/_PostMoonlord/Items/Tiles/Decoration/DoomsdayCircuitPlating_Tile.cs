using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration
{
    public class DoomsdayCircuitPlating_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            //true for block to emit light
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<DoomsdayCircuitPlating>());   
            DustType = ModContent.DustType<Dusts.DoomDust>();
            AddMapEntry(new Color(70, 50, 50
                ));
			MinPick = 225;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(ZAAPlayer.ZeroColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null && tile.HasTile && tile.TileType == Type)
            {
                bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
                Texture2D glowTex = ModContent.Request<Texture2D>(unofficial ? Texture + "_Glow" : "AAModClassic/_Content/Void/_PostMoonlord/Items/Materials/ApocalyptiteOre_Tile_Glow").Value;
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, unofficial ? (color) => Color.Lerp(Color.Red * 0.2f, Color.White * 0.6f, MathF.Sin(Main.GlobalTimeWrappedHourly) / 2f + 0.5f) : AAGlobalTile.GetZeroColorDim);
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}