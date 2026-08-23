using AAModClassic._Unreleased.Content.Mire.World.Tiles;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class MireGrass_Tile : ModTile
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
            AddMapEntry(new Color(0, 50, 140));
            RegisterItemDrop(ItemID.MudBlock);
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.BlueViolet;
            return true;
        }
    }
}