using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class CovetiteOre : ModTile
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
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("CovetiteOre").Type; 
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Covetite");
            DustType = DustID.Gold;
            AddMapEntry(new Color(150, 130, 50), name);
			MinPick = 180;
            HitSound = 21;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}