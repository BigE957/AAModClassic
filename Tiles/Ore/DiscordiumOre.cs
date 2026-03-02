using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class DiscordiumOre : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileOreFinderPriority[Type] = 870; 
            HitSound = 21;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DiscordiumOre").Type;   
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Discordium Ore");
            AddMapEntry(new Color(70, 20, 90), name);
			MinPick = 225;
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
            Main.spriteBatch.Draw(Mod.GetTexture("Glowmasks/DiscordiumOre_Glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), AAColor.Shen3, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}