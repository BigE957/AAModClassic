using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class SkyCrystal : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 825; 
            Main.tileBlendAll[Type] = false;
            HitSound = 21;
            Main.tileLighted[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("SkyCrystal").Type; 
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("SkyCrystal");
            DustType = DustID.BlueCrystalShard;
            AddMapEntry(Color.SkyBlue);
            MinPick = 240;
        }
    }
}