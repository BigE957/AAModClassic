using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Materials
{
    public class HallowedOre_Tile : ModTile
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
            RegisterItemDrop(ModContent.ItemType<HallowedOre>()); 
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Hallowed Ore");
            DustType = DustID.Gold;
            AddMapEntry(new Color(160, 160, 50), name);
			MinPick = 180;
            HitSound = SoundID.Tink;
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