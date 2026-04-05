using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class CovetiteOre_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 680; 
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            RegisterItemDrop(ModContent.ItemType<CovetiteOre>()); 
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Covetite");
            DustType = DustID.Gold;
            AddMapEntry(new Color(150, 130, 50), name);
			MinPick = 180;
            HitSound = SoundID.Tink;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}