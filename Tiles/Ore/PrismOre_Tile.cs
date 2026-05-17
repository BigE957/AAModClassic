using AAModClassic._Content.Underground.___PreHardmode.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class PrismOre_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 420; 
            TileID.Sets.Ore[Type] = true;
            HitSound = SoundID.Tink;
            Main.tileLighted[Type] = true;
            RegisterItemDrop(ModContent.ItemType<Prism>());   
            DustType = DustID.Stone;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Prism Ore");
            AddMapEntry(new Color(100, 100, 100), name);
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
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), new Color(Main.DiscoR / 3, Main.DiscoG / 3, Main.DiscoB / 3), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}