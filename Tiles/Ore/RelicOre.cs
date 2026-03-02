using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class RelicOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileOreFinderPriority[Type] = 370; 
            Main.tileSpelunker[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("VikingRelic").Type;   
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Viking Relic");
            AddMapEntry(new Color(58, 68, 102), name);
			MinPick = 65;
        }
    }
}