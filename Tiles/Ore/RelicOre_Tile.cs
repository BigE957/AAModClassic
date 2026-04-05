using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Ore
{
    public class RelicOre_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileOreFinderPriority[Type] = 370; 
            Main.tileSpelunker[Type] = true;
            RegisterItemDrop(ModContent.ItemType<VikingRelic>());   
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Viking Relic");
            AddMapEntry(new Color(58, 68, 102), name);
			MinPick = 65;
        }
    }
}