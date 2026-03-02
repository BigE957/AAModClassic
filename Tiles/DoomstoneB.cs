using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod.Tiles.Trees;
using Terraria.ID;

namespace AAModClassic.Tiles
{
    public class DoomstoneB : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("Apocalyptite").Type] = true;
            Terraria.ID.TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Tink;
            RegisterItemDrop(Mod.Find<ModItem>("DoomstoneB").Type);   
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            AddMapEntry(new Color(40, 20, 20));
			MinPick = 60;
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}