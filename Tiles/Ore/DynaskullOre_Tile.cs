using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class DynaskullOre_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileOreFinderPriority[Type] = 360; 
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            //true for block to emit light
            HitSound = SoundID.Tink;
            Main.tileLighted[Type] = true;
            RegisterItemDrop(ModContent.ItemType<DynaskullOre>());
            DustType = ModContent.DustType<Dusts.InfinityOverloadY>();
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Dynaskull Ore");
            AddMapEntry(new Color(100, 100, 0), name);
			MinPick = 65;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(Mod.GetTexture("Glowmasks/DynaskullOre_Glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = .250f;
            g = .125f;
            b = 0f;
        }
    }
}