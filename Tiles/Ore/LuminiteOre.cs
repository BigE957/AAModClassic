using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles.Ore
{
    public class LuminiteOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileOreFinderPriority[Type] = 820; 
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ItemID.LunarOre;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Luminite Ore");
            DustType = ModContent.DustType<Dusts.LuminiteDust>();
            HitSound = 21;
            AddMapEntry(new Color(0, 90, 60), name);
			MinPick = 225;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = .90f;
            b = .60f;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}