using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials
{
    public class SkyCrystal_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 825; 
            Main.tileBlendAll[Type] = false;
            HitSound = SoundID.Tink;
            Main.tileLighted[Type] = true;
            RegisterItemDrop(ModContent.ItemType<SkyCrystal>()); 
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("SkyCrystal");
            DustType = DustID.BlueCrystalShard;
            AddMapEntry(Color.SkyBlue);
            MinPick = 240;
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
            Main.spriteBatch.Draw(TextureAssets.Tile[Type].Value, new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), C(), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public static Color C()
        {
            return BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.SkyBlue, Color.Transparent, Color.Transparent, Color.SkyBlue);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = C().R / 150;
            g = C().G / 150;
            b = C().B / 150;
        }
    }
}