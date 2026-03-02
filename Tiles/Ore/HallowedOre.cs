using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Tiles.Ore
{
    public class HallowedOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileOreFinderPriority[Type] = 670; 
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("HallowedOre").Type; 
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Hallowed Ore");
            DustType = DustID.Gold;
            AddMapEntry(new Color(160, 160, 50), name);
			MinPick = 180;
            HitSound = 21;
        }
      
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.5f;
            g = 0.5f;
            b = 0;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}