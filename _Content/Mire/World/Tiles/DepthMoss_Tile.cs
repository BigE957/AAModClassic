using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic._Unreleased.Content.Mire.World.Tiles;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class DepthMoss_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = ModContent.DustType<Dusts.AbyssiumDust>();
            HitSound = SoundID.Tink;
            MinPick = 65;
            AddMapEntry(new Color(0, 50, 140));
            RegisterItemDrop(ModContent.ItemType<Depthstone>());
        }
    }
}