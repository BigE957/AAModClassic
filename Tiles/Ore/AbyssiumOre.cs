using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class AbyssiumOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileOreFinderPriority[Type] = 330; 
            Main.tileSpelunker[Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("Depthstone").Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            HitSound = 21;
            Main.tileLighted[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("Abyssium").Type;   
            DustType = Mod.Find<ModDust>("AbyssiumDust").Type;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Abyssium Ore");
            AddMapEntry(new Color(0, 0, 51), name);
			MinPick = 65;
        }


        public override bool CanExplode(int i, int j)
        {
            return false;
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
            Main.spriteBatch.Draw(Mod.GetTexture("Glowmasks/AbyssiumOre_glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = 0.1f;
            b = 0.25f;
        }
    }
}