using AAModClassic.___Content.Mire.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Materials
{
    public class AbyssiumOre_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileOreFinderPriority[Type] = 330; 
            Main.tileSpelunker[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<Depthstone_Tile>()] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            HitSound = SoundID.Tink;
            Main.tileLighted[Type] = true;
            RegisterItemDrop(ModContent.ItemType<AbyssiumOre>());   
            DustType = ModContent.DustType<Dusts.AbyssiumDust>();
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
            Main.spriteBatch.Draw(Mod.GetTexture("Glowmasks/AbyssiumOre_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = 0.1f;
            b = 0.25f;
        }
    }
}