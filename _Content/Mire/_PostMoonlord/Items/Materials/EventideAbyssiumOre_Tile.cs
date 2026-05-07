using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items.Materials
{
    public class EventideAbyssiumOre_Tile : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 840; 
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            HitSound = SoundID.Tink;
            TileID.Sets.JungleSpecial[Type] = true;
            Main.tileLighted[Type] = true;
            RegisterItemDrop(ModContent.ItemType<EventideAbyssiumOre>());   
            DustType = ModContent.DustType<Dusts.YamataDust>();
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Eventide Abyssium Ore");
            AddMapEntry(new Color(0, 0, 30), name);
			MinPick = 225;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/EventideAbyssiumOre_Glow").Value;
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetYamataColorDim2);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = .2f;
            g = 0f;
            b = .5f;
        }
    }
}