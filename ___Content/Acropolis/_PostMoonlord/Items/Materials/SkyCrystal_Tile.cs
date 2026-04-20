using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Acropolis._PostMoonlord.Items.Materials
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
    }
}